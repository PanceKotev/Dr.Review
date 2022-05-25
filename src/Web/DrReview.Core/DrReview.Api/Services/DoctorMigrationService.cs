namespace DrReview.Api.Services
{
    using System.Data.SqlClient;
    using Dapper;
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Common.Extensions;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Doctor.Entities;
    using DrReview.Contracts.Storage.Institution.Entities;
    using DrReview.Contracts.Storage.Location.Entities;
    using DrReview.Contracts.Storage.Specialization.Entities;

    public class DoctorMigrationService : IDoctorMigrationService
    {
        private readonly IMojTerminHttpClient _mojTerminHttpClient;
        private readonly string _connectionString;

        public DoctorMigrationService(IMojTerminHttpClient mojTerminHttpClient, string connectionString)
        {
            _mojTerminHttpClient = mojTerminHttpClient;
            _connectionString = connectionString;
        }

        public async Task PopulateLocationsAsync()
        {
            List<LocationResponse> locations = await _mojTerminHttpClient.GetLocationsAsync();

            if (!locations.Any())
            {
                throw new DataMisalignedException("No locations");
            }

            List<Location> locationsToInsert = locations.Select(l => Location.FromResponse(l)).ToList();

            string insertLocationsQuery = @"
                        IF NOT EXISTS (SELECT * FROM [dbo].[Location] WHERE ID = @Id) 
                        INSERT [dbo].[Location](ID, Uid, Suid, DeletedOn, ModifiedOn, Name, Longitude, Latitude) VALUES(@Id, @Uid, @Suid, @DeletedOn, @ModifiedOn, @Name, @Longitude, @Latitude)";

            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync(insertLocationsQuery, locationsToInsert);

                await connection.CloseAsync();
            }
        }

        public async Task MigrateDoctorDataAsync()
        {
            Dictionary<long, long> res = await _mojTerminHttpClient.GetInstitutionsAsync();

            int maxRequests = 17;
            int maxPages = 100;

            for (int page = 0; page < maxPages; page++)
            {
                Dictionary<long, long> cutOffDictionary = res.GetRangedDictionary(page * maxRequests, maxRequests);

                List<DoctorResponse> doctorResponses = _mojTerminHttpClient.GetDoctorsInInstitutions(cutOffDictionary);

                Specialization[] specializations = doctorResponses.Select(x => Specialization.FromName(x.Group)).ToArray();

                Institution[] institutions = doctorResponses.Select(x => Institution.FromResponse(x.Institution)).ToArray();

                IEnumerable<Institution> institutionsWithoutLocationIds = institutions.Where(i => i.LocationFK == 0);

                int totalSpecializations = specializations.Length;

                long[] specializationIds = new long[totalSpecializations];

                using SqlConnection connection1 = new SqlConnection(_connectionString);
                {
                    await connection1.OpenAsync();

                    string insertSpecializationsQuery = @"IF NOT EXISTS (SELECT * FROM [dbo].[Specialization] WHERE Name = @Name)
                          INSERT [dbo].[Specialization](Uid, Suid, DeletedOn, ModifiedOn, Name) 
                          OUTPUT INSERTED.ID
                          VALUES(@Uid, @Suid, @DeletedOn, @ModifiedOn, @Name)";

                    string insertInstitutionsQuery = @"
                        IF NOT EXISTS (SELECT * FROM [dbo].[Institution] WHERE ID = @Id) 
                        INSERT [dbo].[Institution](ID, Uid, Suid, DeletedOn, ModifiedOn, Name, LocationFK) VALUES(@Id, @Uid, @Suid, @DeletedOn, @ModifiedOn, @Name, @LocationFK)";

                    string findIdOfSpecializationQuery = @"SELECT ID FROM [dbo].[Specialization] WHERE Name = @Name";

                    for (int i = 0; i < totalSpecializations; i++)
                    {
                        specializationIds[i] = await connection1.ExecuteScalarAsync<long>(insertSpecializationsQuery, specializations[i]);

                        if (specializationIds[i] == 0)
                        {
                            specializationIds[i] = await connection1.ExecuteScalarAsync<long>(findIdOfSpecializationQuery, specializations[i]);
                        }
                    }

                    await connection1.ExecuteAsync(insertInstitutionsQuery, institutions);



                    await connection1.CloseAsync();
                }

                long[] specializationId = specializationIds.ToArray();

                List<Doctor> doctorsToInsert = doctorResponses.Select((x, index) => Doctor.FromResponse(x, specializationId[index])).ToList();

                using SqlConnection connection2 = new SqlConnection(_connectionString);
                {
                    await connection2.OpenAsync();

                    string insertDoctorsCommand = @"
                        IF NOT EXISTS (SELECT * FROM dbo.Doctor WHERE ID = @Id) 
                        INSERT dbo.Doctor(ID, Uid, Suid, DeletedOn, ModifiedOn, FirstName, LastName, SpecializationFK, InstitutionFK) VALUES(@Id, @Uid, @Suid, @DeletedOn, @ModifiedOn, @FirstName, @LastName, @SpecializationFK, @InstitutionFK)";

                    await connection2.ExecuteAsync(insertDoctorsCommand, doctorsToInsert);

                    await connection2.CloseAsync();
                }
            }


        }
    }
}
