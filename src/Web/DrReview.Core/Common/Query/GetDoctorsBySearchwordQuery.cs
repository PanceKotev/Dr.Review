namespace DrReview.Common.Query
{
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Entities;

    public class GetDoctorsBySearchwordQuery : IQuery<Result<List<SearchDoctorDto>>>
    {
        public GetDoctorsBySearchwordQuery(string searchword, int startPage, int offset)
        {
            Searchword = searchword;
            StartPage = startPage;
            Offset = offset;
        }

        public string Searchword { get; init; }

        public int StartPage { get; init; }

        public int Offset { get; init; }
    }

    public class GetDoctorsBySearchwordQueryHandler : IQueryHandler<GetDoctorsBySearchwordQuery, Result<List<SearchDoctorDto>>>
    {
        private readonly IConfiguration _configuration;

        public GetDoctorsBySearchwordQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<SearchDoctorDto>>> Handle(GetDoctorsBySearchwordQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string procedure = "[dbo].[SearchDoctorsBySearchword]";
            List<Doctor> results = connection.Query<Doctor>(procedure, new { searchword = request.Searchword }, commandType: CommandType.StoredProcedure).ToList();

            
            if(results is null)
            {
                return Result.Invalid<List<SearchDoctorDto>>("Error");
            }

            List<long> institutitonIds = results.Select(x => x.InstitutionFK).ToList();
            string sqlForInstitutions = $@"SELECT * FROM [dbo].[Institution] WHERE ID IN @Ids";

            Dictionary<long, string> insitutionDict = connection.Query(sqlForInstitutions, new { Ids = institutitonIds }).ToDictionary(row => (long)row.ID, row => (string)row.Name);

            List<long> specializationIds = results.Select(x => x.SpecializationFK).ToList();
            string sqlForSpecializations = $@"SELECT * FROM [dbo].[Specialization] WHERE ID IN @Ids";

            Dictionary<long, string> specializationDict = connection.Query(sqlForSpecializations, new { Ids = specializationIds }).ToDictionary(row => (long)row.ID, row => (string)row.Name);

            List<SearchDoctorDto> mappedResults = results.Select(d => new SearchDoctorDto(
                                                                          suid: d.Suid,
                                                                          firstName: d.FirstName,
                                                                          lastName: d.LastName,
                                                                          institution: insitutionDict.GetValueOrDefault(d.InstitutionFK)!,
                                                                          specialization: specializationDict.GetValueOrDefault(d.SpecializationFK)!)).ToList();

            await connection.CloseAsync();

            return Result.Ok(mappedResults);
        }
    }
}
