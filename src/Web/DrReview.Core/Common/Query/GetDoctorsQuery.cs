namespace DrReview.Common.Query
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Entities;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Contracts.Filters;
    using DrReview.Contracts.Filters.Enums;
    using DrReview.Contracts.Storage.Institution.Entities;
    using DrReview.Contracts.Storage.Location.Entities;
    using DrReview.Contracts.Storage.Specialization.Entities;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorsQuery : IQuery<Result<List<SearchDoctorDto>>>
    {
        public GetDoctorsQuery(GetDoctorsFilter filter)
        {
            Filter = filter;
        }

        public GetDoctorsFilter Filter { get; }
    }

    public class GetDoctorsQueryHandler : IQueryHandler<GetDoctorsQuery, Result<List<SearchDoctorDto>>>
    {
        private readonly IConfiguration _configuration;

        public GetDoctorsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<SearchDoctorDto>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {

            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string baseSqlForDoctors = $@"SELECT D.Suid as Suid, D.FirstName as FirstName, D.LastName as LastName, I.Name as Institution, S.Name as Specialization FROM [dbo].[Doctor] as D INNER JOIN 
            [dbo].[Institution] AS I ON I.ID = D.InstitutionFK INNER JOIN [dbo].[Location] AS L ON I.LocationFK = L.ID INNER JOIN [dbo].[Specialization] AS S ON D.SpecializationFK = S.ID";

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

            List<SearchDoctorDto> results = (await connection.QueryAsync<SearchDoctorDto>(
               $@"{baseSqlForDoctors} {whereBlock}", new { filterValue = request.Filter?.FilterBy?.Value.Trim() ?? string.Empty })).ToList();

            await connection.CloseAsync();

            return Result.Ok(results);
        }
    }
}
