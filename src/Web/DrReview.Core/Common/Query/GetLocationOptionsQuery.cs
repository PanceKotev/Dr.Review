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
    using DrReview.Contracts.Filters;
    using Microsoft.Extensions.Configuration;

    public class GetLocationOptionsQuery : IQuery<Result<List<GetLocationOptionsDto>>>
    {
        public GetLocationOptionsQuery()
        {
        }

    }

    public class GetLocationOptionsQueryHandler : IQueryHandler<GetLocationOptionsQuery, Result<List<GetLocationOptionsDto>>>
    {
        private readonly IConfiguration _configuration;

        public GetLocationOptionsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<GetLocationOptionsDto>>> Handle(GetLocationOptionsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string queryForLocations = $@"SELECT L.Suid as Suid, L.Longitude AS Longitude, L.Latitude as Latitude, L.Name as Name FROM [dbo].[Location] as L
                                        WHERE L.DeletedOn IS NULL";

            List<GetLocationOptionsDto> result = (await connection.QueryAsync<GetLocationOptionsDto>(queryForLocations)).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
