#nullable disable
namespace DrReview.Common.Entities
{
    using System;
    using System.Text.Json.Serialization;

    public class Doctor
    {
        [JsonPropertyName("ID")]
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public string Suid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public long InstitutionFK { get; set; }

        public long SpecializationFK { get; set; }
    }
}
#nullable enable
