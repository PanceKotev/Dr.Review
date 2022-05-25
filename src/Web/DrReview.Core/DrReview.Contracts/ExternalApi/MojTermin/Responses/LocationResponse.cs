namespace DrReview.Contracts.ExternalApi.MojTermin.Responses
{
    using System;

    public class LocationResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }


    public class LocationCoordinatesResponse
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
