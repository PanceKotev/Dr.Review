namespace DrReview.Common.Dtos.Doctor
{

    public class GetLocationOptionsDto
    {
        public GetLocationOptionsDto(decimal longitude, decimal latitude, string name)
        {
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
        }

        public decimal Longitude { get; init; }

        public decimal Latitude { get; init; }

        public string Name { get; init; }
    }
}
