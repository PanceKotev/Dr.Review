namespace DrReview.Contracts.Storage.Doctor.Entities
{
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using System;

    public class Doctor
    {
        public Doctor(
            long id,
            Guid uid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            string firstName,
            string lastName,
            string occupation,
            long ordinationFK)
        {
            Id = id;
            Uid = uid;
            DeletedOn = deletedOn;
            ModifiedOn = modifiedOn;
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
            OrdinationFK = ordinationFK;
        }

        public long Id { get; init; }

        public Guid Uid { get; init; }

        public DateTime? DeletedOn { get; init; }

        public DateTime ModifiedOn { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Occupation { get; init; }

        public long OrdinationFK { get; init; }

        public static Doctor FromResponse(DoctorResponse response, long ordinationId)
        {
            string[] nameSplitUp = response.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return new Doctor(
                    id: response.Id,
                    uid: Guid.NewGuid(),
                    deletedOn: null,
                    modifiedOn: DateTime.UtcNow,
                    firstName: nameSplitUp[0],
                    lastName: nameSplitUp[1],
                    occupation: response.Group,
                    ordinationFK: ordinationId);
        }
    }
}
