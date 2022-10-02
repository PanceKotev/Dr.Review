namespace DrReview.Common.Query
{
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorsCountQuery : IQuery<Result<long>>
    {
    }

    public class GetDoctorsCountQueryHandler : IQueryHandler<GetDoctorsCountQuery, Result<long>>
    {
        private readonly IConfiguration _configuration;

        public GetDoctorsCountQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<long>> Handle(GetDoctorsCountQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string countQuery = "SELECT COUNT(*) FROM [dbo].[Doctor] as D WHERE D.DeletedOn IS NULL";
            long count = await connection.ExecuteScalarAsync<int>(countQuery);

            await connection.CloseAsync();

            return Result.Ok(count);
        }
    }
}
