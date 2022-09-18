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

    public class GetSpecializationOptionsQuery : IQuery<Result<List<string>>>
    {
        public GetSpecializationOptionsQuery()
        {
        }

    }

    public class GetSpecializationOptionsQueryHandler : IQueryHandler<GetSpecializationOptionsQuery, Result<List<string>>>
    {
        private readonly IConfiguration _configuration;

        public GetSpecializationOptionsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<string>>> Handle(GetSpecializationOptionsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string queryForSpecializations = $@"SELECT S.Name FROM [dbo].[Specialization] as S
                                        WHERE S.DeletedOn IS NULL";

            List<string> result = (await connection.QueryAsync<string>(queryForSpecializations)).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
