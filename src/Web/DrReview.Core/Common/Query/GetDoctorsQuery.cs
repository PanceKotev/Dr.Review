namespace DrReview.Common.Query
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Filters.Enums;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorsQuery : IQuery<Result<GetDoctorsDto>>
    {
        public GetDoctorsQuery(GetDoctorsFilter filter, bool withSubscriptions = false)
        {
            Filter = filter;
            WithSubscriptions = withSubscriptions;
        }

        public GetDoctorsFilter Filter { get; }

        public bool WithSubscriptions { get; } = false;
    }

    public class GetDoctorsQueryHandler : IQueryHandler<GetDoctorsQuery, Result<GetDoctorsDto>>
    {
        private readonly IConfiguration _configuration;

        public GetDoctorsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<GetDoctorsDto>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string baseSqlForDoctors = $@"SELECT D.Suid as Suid, D.FirstName as FirstName, D.LastName as LastName, I.Name as Institution, S.Name as Specialization FROM [dbo].[Doctor] as D INNER JOIN 
            [dbo].[Institution] AS I ON I.ID = D.InstitutionFK INNER JOIN [dbo].[Location] AS L ON I.LocationFK = L.ID INNER JOIN [dbo].[Specialization] AS S ON D.SpecializationFK = S.ID";

            string countForDoctors = $@"SELECT COUNT(D.ID) FROM [dbo].[Doctor] as D INNER JOIN [dbo].[Institution] AS I ON I.ID = D.InstitutionFK INNER JOIN [dbo].[Location] AS L ON I.LocationFK = L.ID INNER JOIN [dbo].[Specialization] AS S ON D.SpecializationFK = S.ID";

            string whereBlock = $@"WHERE D.DeletedOn IS NULL";

            if (request.Filter?.FilterBy?.Property == FilterBy.INSTITUTION)
            {
                whereBlock = $@"{whereBlock} AND I.NAME = @filterValue";
            }

            if (request.Filter?.FilterBy?.Property == FilterBy.LOCATION)
            {
                whereBlock = $@"{whereBlock} AND L.NAME = @filterValue";
            }

            if (request.Filter?.FilterBy?.Property == FilterBy.SPECIALIZATION)
            {
                whereBlock = $@"{whereBlock} AND S.NAME = @filterValue";
            }

            List<GetDoctorDto> results = (await connection.QueryAsync<GetDoctorDto>(
               $@"{baseSqlForDoctors} {whereBlock} ORDER BY D.ReviewScore OFFSET @skip ROWS
	               FETCH NEXT @take ROWS ONLY ", new
                    {
                       filterValue = request.Filter?.FilterBy?.Value.Trim() ?? string.Empty,
                       skip = request.Filter != null ? request.Filter.StartPage * request.Filter.ItemsPerPage : 0,
                       take = request.Filter != null ? request.Filter.ItemsPerPage : 10000
                    })).ToList();

            List<string> doctorSuids = results.Select(x => x.Suid).ToList();

            string sqlForSchedules = $@"SELECT SC.*, D.Suid AS DoctorSuid FROM [dbo].[ScheduleSubscription] AS SC INNER JOIN [dbo].[Doctor] AS D ON D.ID = SC.DoctorFK WHERE SC.DeletedOn IS NULL AND D.Suid IN @Suids";

            Dictionary<string, GetDoctorScheduleSubscriptionDto> scheduleDict = request.WithSubscriptions ? 
                (await connection.QueryAsync(sqlForSchedules, new { Suids = doctorSuids })).ToDictionary(row => (string)row.DoctorSuid, row => new GetDoctorScheduleSubscriptionDto(row.Suid, new ScheduleSubscriptionRangeDto(DateOnly.FromDateTime(row.RangeFrom), DateOnly.FromDateTime(row.RangeTo), true))) 
                : new ();

            long doctorCount = await connection.ExecuteScalarAsync<long>(
                $@"{countForDoctors} {whereBlock}", new
                {
                    filterValue = request.Filter?.FilterBy?.Value.Trim() ?? string.Empty
                });

            foreach (GetDoctorDto doctor in results)
            {
                doctor.ScheduleSubscription = scheduleDict.GetValueOrDefault(doctor.Suid);
            }

            GetDoctorsDto result = new GetDoctorsDto(results ?? new List<GetDoctorDto>(), doctorCount);

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
