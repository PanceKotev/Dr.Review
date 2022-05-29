namespace DrReview.Contracts.Storage.Location.Entities
{
    using System;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Common;

    public class Location : BaseEntity
    {
        public Location(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string name, decimal longitude, decimal latitude)
            : base(id, uid, deletedOn, modifiedOn)
        {
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
        }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public string Name { get; init; }

        public static Location FromResponse(LocationResponse response)
        {
            return new Location(
                id: response.Id,
                uid: Guid.NewGuid(),
                deletedOn: null,
                modifiedOn: DateTime.UtcNow,
                name: response.Name,
                longitude: response.Coordinates.Longitude,
                latitude: response.Coordinates.Latitude);
        }
    }
}
