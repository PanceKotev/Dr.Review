namespace DrReview.Contracts.Storage.Institution.Entities
{
    using System;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Common;

    public class Institution : BaseEntity
    {
#nullable disable
        public Institution()
            : base(0, Guid.Empty, null, DateTime.UtcNow)
        {
        }
#nullable enable

        public Institution(long id, Guid uid, DateTime? deletedOn, DateTime modifiedOn, string name, long locationFK)
            : base(id, uid, deletedOn, modifiedOn)
        {
            Name = name;
            LocationFK = locationFK;
        }

        public string Name { get; set; }

        public long LocationFK { get; set; }

        public static Institution FromResponse(InstitutionResponse response)
        {
            return new Institution(
                id: response.Id,
                uid: Guid.NewGuid(),
                deletedOn: null,
                modifiedOn: DateTime.UtcNow,
                name: response.Name,
                locationFK: response.LocationId);
        }
    }
}
