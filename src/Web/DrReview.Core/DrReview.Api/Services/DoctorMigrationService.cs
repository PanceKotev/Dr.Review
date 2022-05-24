namespace DrReview.Api.Services
{
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Doctor.Entities;
    using System.Data.SqlClient;
    using Dapper;
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

        public async Task MigrateDoctorDataAsync()
        {
            List<long> res = await _mojTerminHttpClient.GetInstitutionsAsync();

            int maxRequests = 17;
            int maxPages = 2;

            for (int page = 0; page < maxPages; page++)
            {
                long[] institutionIds = res.GetRange(page * maxRequests, maxRequests).ToArray();
                List<Doctor> doctorsToInsert = new List<Doctor>();
                List<DoctorResponse> doctorResponses = new List<DoctorResponse>();
                doctorResponses = _mojTerminHttpClient.GetDoctorsInInstitutions(institutionIds);

                IEnumerable<Specialization> specializations = doctorResponses.Select(x => Specialization.FromName(x.Group));

                IEnumerable<long> specializationIds = Enumerable.Empty<long>();

                using SqlConnection connection1 = new SqlConnection(_connectionString);
                {
                    await connection1.OpenAsync();

                    specializationIds = (await connection1.QueryAsync<long>(@"
                           IF NOT EXISTS (SELECT * FROM dbo.Specialization WHERE Name = @Name)
                           INSERT dbo.Specialization(Uid, Suid, DeletedOn, ModifiedOn, Name) VALUES(@Uid, @Suid, @DeletedOn, @ModifiedOn, @Name)
                           OUTPUT INSERTED.ID", specializations));

                    //await connection.ExecuteAsync(@"
                    //    SET IDENTITY_INSERT dbo.Institution ON
                    //    IF NOT EXISTS (SELECT * FROM dbo.Institution WHERE ID = @Id)
                    //    INSERT dbo.Institution(ID, Uid, Suid, DeletedOn, ModifiedOn, Name, LocationFK) VALUES(@Id, @Uid, @Suid, @DeletedOn, @ModifiedOn, @Name, @LocationFK)"
                    //    ,);

                    await connection1.CloseAsync();
                }
                long[] specializationId = specializationIds.ToArray();

                doctorsToInsert = doctorResponses.Select((x, index) => Doctor.FromResponse(x, specializationId[index])).ToList();


                using SqlConnection connection2 = new SqlConnection(_connectionString);
                {
                    await connection2.OpenAsync();

                    await connection2.ExecuteAsync(@"
                        SET IDENTITY_INSERT dbo.Doctor ON
                        IF NOT EXISTS (SELECT * FROM dbo.Doctor WHERE ID = @Id) 
                        INSERT dbo.Doctor(ID, Uid, Suid, DeletedOn, ModifiedOn, FirstName, LastName, SpecializationFK, InstitutionFK) VALUES(@Id, @Uid, @Suid, @DeletedOn, @ModifiedOn, @FirstName, @LastName, @SpecializationFK, @InstitutionFK)
                        SET IDENTITY_INSERT dbo.Doctor OFF", doctorsToInsert);

                    await connection.CloseAsync();
                }
            }


        }
    }
}
