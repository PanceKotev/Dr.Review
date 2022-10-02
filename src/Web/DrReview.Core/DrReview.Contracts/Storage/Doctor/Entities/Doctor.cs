namespace DrReview.Contracts.Storage.Doctor.Entities
{
    using System;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;
    using DrReview.Contracts.Storage.Common;

    public class Doctor : BaseEntity
    {
        public Doctor(
            long id,
            Guid uid,
            DateTime? deletedOn,
            DateTime modifiedOn,
            string firstName,
            string lastName,
            long specializationFK,
            long institutionFK)
            : base(id, uid, deletedOn, modifiedOn)
        {
            FirstName = firstName;
            LastName = lastName;
            SpecializationFK = specializationFK;
            InstitutionFK = institutionFK;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long SpecializationFK { get; set; }

        public long InstitutionFK { get; set; }

        public static Doctor FromResponse(DoctorResponse response, long specializationFK)
        {
            string[] nameSplitUp = response.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return new Doctor(
                    id: response.Id,
                    uid: Guid.NewGuid(),
                    deletedOn: null,
                    modifiedOn: DateTime.UtcNow,
                    firstName: nameSplitUp[0],
                    lastName: nameSplitUp[1],
                    specializationFK: specializationFK,
                    institutionFK: response.InstitutionFK);
        }
    }
}
