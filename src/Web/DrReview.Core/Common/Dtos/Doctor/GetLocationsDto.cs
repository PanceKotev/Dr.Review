namespace DrReview.Common.Dtos.Doctor
{

    public class GetLocationOptionsDto
    {
        public GetLocationOptionsDto(string suid, decimal longitude, decimal latitude, string name)
        {
            Suid = suid;
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
        }

        public string Suid { get; init; }

        public decimal Longitude { get; init; }

        public decimal Latitude { get; init; }

        public string Name { get; init; }
    }
}
