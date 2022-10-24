namespace DrReview.Common.Query
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorDetailsQuery : IQuery<Result<GetDoctorDetailsDto>>
    {
        public GetDoctorDetailsQuery(string suid)
        {
            Suid = suid;
        }

        public string Suid { get; }
    }

    public class GetDoctorDetailsQueryHandler : IQueryHandler<GetDoctorDetailsQuery, Result<GetDoctorDetailsDto>>
    {
        private readonly IConfiguration _configuration;

        private readonly ICurrentUser _currentUser;

        public GetDoctorDetailsQueryHandler(
            ICurrentUser currentUser,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _currentUser = currentUser;
        }

        public async Task<Result<GetDoctorDetailsDto>> Handle(GetDoctorDetailsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");

            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string queryForDoctors = "SELECT TOP(1) D.Suid as suid, D.FirstName as firstName, D.LastName AS lastName, I.Name AS institution, S.Name AS specialization, L.Name as location  FROM [dbo].[Doctor] AS D " +
                                     " INNER JOIN [dbo].[Institution] AS I ON D.InstitutionFK = I.ID" +
                                     " INNER JOIN [dbo].[Location] AS L ON L.ID = I.LocationFK" +
                                     " INNER JOIN [dbo].[Specialization] AS S ON S.ID = D.SpecializationFK" +
                                     " WHERE D.Suid = @Suid AND D.DeletedOn IS NULL";

            GetDoctorDetailsDto? result = await connection.QueryFirstOrDefaultAsync<GetDoctorDetailsDto>(queryForDoctors, new { Suid = request.Suid });

            if (result is null)
            {
                return Result.NotFound<GetDoctorDetailsDto>("Doctor with that suid not found");
            }

            string queryForSubscription = "SELECT S.Suid as scheduleSuid, S.RangeFrom as 'from', S.RangeTo as 'to', CAST(1 AS bit) as subscribedTo " +
                "FROM [dbo].[ScheduleSubscription] as S INNER JOIN [dbo].[User] AS U ON U.ID = S.UserFK" +
                " INNER JOIN [dbo].[Doctor] AS D ON D.ID = S.DoctorFK" +
                " WHERE U.Uid = @CurrentUserUid AND S.DeletedOn IS NULL AND D.Suid = @DoctorSuid";

            GetDoctorDetailsScheduleSubscriptionDto? range = await connection.QueryFirstOrDefaultAsync<GetDoctorDetailsScheduleSubscriptionDto>(
                queryForSubscription,
                new
                {
                    CurrentUserUid = _currentUser.Uid,
                    DoctorSuid = result.Suid
                });

            result.ScheduleSubscription = range;

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
