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
    using DrReview.Contracts.Storage.Location.Entities;
    using Microsoft.Extensions.Configuration;

    public class GetTopDoctorsNearLocationQuery : IQuery<Result<List<GetTopDoctorsDto>>>
    {
        public GetTopDoctorsNearLocationQuery(string locationSuid, int distance, int numberOfDoctors)
        {
            LocationSuid = locationSuid;
            Distance = distance;
            NumberOfDoctors = numberOfDoctors;
        }

        public string LocationSuid { get; init; }

        public int Distance { get; init; }

        public int NumberOfDoctors { get; init; }
    }

    public class GetTopDoctorsNearLocationQueryHandler : IQueryHandler<GetTopDoctorsNearLocationQuery, Result<List<GetTopDoctorsDto>>>
    {
        private readonly IConfiguration _configuration;

        public GetTopDoctorsNearLocationQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<List<GetTopDoctorsDto>>> Handle(GetTopDoctorsNearLocationQuery request, CancellationToken cancellationToken)
        {
            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            string queryForLocation = $@"SELECT TOP(1) L.* FROM [dbo].[Location] AS L WHERE L.Suid = @locationSuid AND L.DeletedOn IS NULL";

            Location? location = await connection.QueryFirstOrDefaultAsync<Location>(
                queryForLocation,
                new { locationSuid = request.LocationSuid });

            if (location is null)
            {
                await connection.CloseAsync();

                return Result.NotFound<List<GetTopDoctorsDto>>(ResultCodes.LocationNotFound);
            }

            string queryForTopDoctors = $@"SELECT TOP(@doctorLimit) D.Suid as Suid, D.FirstName as FirstName, D.LastName as LastName,
                I.Name as Institution, S.Name as Specialization, L.Name as Location,  ABS([dbo].[DistanceKM](@lat, @lng, L.Latitude, L.Longitude)) AS Distance
                  FROM [DrReview.Database].[dbo].[Doctor] AS D
                  INNER JOIN [dbo].[Institution] AS I ON D.InstitutionFK = I.ID
                  INNER JOIN [dbo].[Specialization] AS S ON D.SpecializationFK = S.ID
                  INNER JOIN [dbo].[Location] AS L ON L.ID = I.LocationFK AND
                  ABS([dbo].[DistanceKM](@lat, @lng, L.Latitude, L.Longitude)) <= @distance";

            List<GetTopDoctorsDto> result = (await connection.QueryAsync<GetTopDoctorsDto>(queryForTopDoctors, new
            {
                doctorLimit = request.NumberOfDoctors,
                lat = location.Latitude,
                lng = location.Longitude,
                distance = request.Distance
            })).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
