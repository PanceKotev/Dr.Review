namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    using System;

    public class LocationResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public LocationCoordinatesResponse Coordinates { get; set; }
    }


    public class LocationCoordinatesResponse
    {
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
