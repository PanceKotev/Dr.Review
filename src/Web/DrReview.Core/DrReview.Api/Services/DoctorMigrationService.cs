namespace DrReview.Api.Services
{
    using DrReview.Api.HttpClients.MojTermin.Interfaces;
    using DrReview.Api.Services.Interfaces;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Doctor.Entities;
    using System.Data.SqlClient;
    using Dapper;

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
            int maxPages = 5;


            for (int page = 0; page < maxPages; page++)
            {
                long[] institutionIds = res.GetRange(page * maxRequests, maxRequests).ToArray();
                List<Doctor> doctorsToInsert = new List<Doctor>();
                List<DoctorResponse> doctorResponses = new List<DoctorResponse>();
                doctorResponses = _mojTerminHttpClient.GetDoctorsInInstitutions(institutionIds);

                doctorsToInsert = doctorResponses.Select(x => Doctor.FromResponse(x, 1L)).ToList();

                using SqlConnection connection = new SqlConnection(_connectionString);
                {
                    await connection.OpenAsync();

                    await connection.ExecuteAsync(@"
                        SET IDENTITY_INSERT dbo.Doctor ON
                        IF NOT EXISTS (SELECT * FROM dbo.Doctor WHERE ID = @Id) 
                        INSERT dbo.Doctor(ID, Uid, DeletedOn, ModifiedOn, FirstName, LastName, Occupation, OrdinationFK) VALUES(@Id, @Uid, @DeletedOn, @ModifiedOn, @FirstName, @LastName, @Occupation, @OrdinationFK)
                        SET IDENTITY_INSERT dbo.Doctor OFF", doctorsToInsert);

                    await connection.CloseAsync();
                }
            }


        }
    }
}
