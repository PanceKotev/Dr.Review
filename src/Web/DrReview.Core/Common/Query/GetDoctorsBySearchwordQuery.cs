namespace DrReview.Common.Query
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Dtos.Doctor;
    using DrReview.Common.Entities;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using Microsoft.Extensions.Configuration;

    public class GetDoctorsBySearchwordQuery : IQuery<Result<List<SearchDoctorDto>>>
    {
        public GetDoctorsBySearchwordQuery(string? searchword, bool filterSchedules = false)
        {
            Searchword = searchword;
            FilterSchedules = filterSchedules;
        }

        public string? Searchword { get; init; }

        public bool FilterSchedules { get; init; } = false;
    }

    public class GetDoctorsBySearchwordQueryHandler : IQueryHandler<GetDoctorsBySearchwordQuery, Result<List<SearchDoctorDto>>>
    {
        private readonly IConfiguration _configuration;

        private readonly ICurrentUser _currentUser;

        public GetDoctorsBySearchwordQueryHandler(
            IConfiguration configuration,
            ICurrentUser currentUser)
        {
            _configuration = configuration;
            _currentUser = currentUser;
        }

        public async Task<Result<List<SearchDoctorDto>>> Handle(GetDoctorsBySearchwordQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Searchword))
            {
                return Result.Ok<List<SearchDoctorDto>>(new ());
            }

            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            string procedure = "[dbo].[SearchDoctorsBySearchword]";
            List<Doctor> results = connection.Query<Doctor>(
                procedure,
                new
                {
                    searchword = request.Searchword.Trim(),
                    currentUserUid = _currentUser.Uid,
                    filterSchedules = request.FilterSchedules
                },
                commandType: CommandType.StoredProcedure).ToList();

            if (results is null)
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
                                                                          institution: insitutionDict.GetValueOrDefault(d.InstitutionFK) ?? string.Empty,
                                                                          specialization: specializationDict.GetValueOrDefault(d.SpecializationFK) ?? string.Empty)).ToList();

            await connection.CloseAsync();

            return Result.Ok(mappedResults);
        }
    }
}
