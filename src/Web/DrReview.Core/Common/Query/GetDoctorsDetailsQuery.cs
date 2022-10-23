namespace DrReview.Common.Query
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorsDetailsQuery : IQuery<Result<List<GetDoctorDetailsDto>>>
    {
        public GetDoctorsDetailsQuery(List<string> doctorSuids)
        {
            DoctorSuids = doctorSuids;
        }

        public List<string> DoctorSuids { get; }
    }

    public class GetDoctorsDetailsQueryHandler : IQueryHandler<GetDoctorsDetailsQuery, Result<List<GetDoctorDetailsDto>>>
    {
        private readonly IConfiguration _configuration;

        public GetDoctorsDetailsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<GetDoctorDetailsDto>>> Handle(GetDoctorsDetailsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");

            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string queryForDoctors = "SELECT  D.Suid as suid, D.FirstName as firstName, D.LastName AS lastName, I.Name AS institution, S.Name AS specialization, L.Name as location FROM [dbo].[Doctor] AS D " +
                                     " INNER JOIN [dbo].[Institution] AS I ON D.InstitutionFK = I.ID" +
                                     " INNER JOIN [dbo].[Location] AS L ON L.ID = I.LocationFK" +
                                     " INNER JOIN [dbo].[Specialization] AS S ON S.ID = D.SpecializationFK" +
                                     " WHERE D.Suid IN @suids";

            List<GetDoctorDetailsDto> result = (await connection.QueryAsync<GetDoctorDetailsDto>(queryForDoctors, new { suids = request.DoctorSuids.ToArray() })).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
