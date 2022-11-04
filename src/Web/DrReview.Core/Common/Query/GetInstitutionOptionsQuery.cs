namespace DrReview.Common.Query
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using Microsoft.Extensions.Configuration;

    public class GetInstitutionOptionsQuery : IQuery<Result<List<string>>>
    {
        public GetInstitutionOptionsQuery()
        {
        }
    }

    public class GetInstitutionOptionsQueryHandler : IQueryHandler<GetInstitutionOptionsQuery, Result<List<string>>>
    {
        private readonly IConfiguration _configuration;

        public GetInstitutionOptionsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<string>>> Handle(GetInstitutionOptionsQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string queryForInstitutions = $@"SELECT I.Name FROM [dbo].[Institution] as I
                                        WHERE I.DeletedOn IS NULL";

            List<string> result = (await connection.QueryAsync<string>(queryForInstitutions)).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
