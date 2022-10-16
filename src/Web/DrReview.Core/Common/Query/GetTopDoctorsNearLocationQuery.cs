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
        public GetTopDoctorsNearLocationQuery(decimal latitude, decimal longitude,  int distance, int numberOfDoctors)
        {
            Latitude = latitude;
            Longitude = longitude;
            Distance = distance;
            NumberOfDoctors = numberOfDoctors;
        }

        public decimal Latitude { get; init; }

        public decimal Longitude { get; init; }

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
            bool validLatitude = request.Latitude <= 90 && request.Latitude >= -90;
            bool validLongitude = request.Longitude <= 180 && request.Longitude >= -180;

            if (!validLatitude || !validLongitude)
            {
                return Result.Invalid<List<GetTopDoctorsDto>>(ResultCodes.LocationNotFound);
            }

            string connectionString = _configuration.GetConnectionString("DatabaseConnection");
            using SqlConnection connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

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
                lat = request.Latitude,
                lng = request.Longitude,
                distance = request.Distance
            })).ToList();

            await connection.CloseAsync();

            return Result.Ok(result);
        }
    }
}
