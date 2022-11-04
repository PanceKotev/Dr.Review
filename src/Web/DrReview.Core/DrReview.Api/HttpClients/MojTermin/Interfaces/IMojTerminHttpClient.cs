namespace DrReview.Api.HttpClients.MojTermin.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;

    public interface IMojTerminHttpClient
    {
        Task<Dictionary<long, long>> GetInstitutionsAsync();

        List<DoctorResponse> GetDoctorsInInstitutions(Dictionary<long, long> instiutionLocationMap);

        Task<List<LocationResponse>> GetLocationsAsync();

        Task<List<TimeslotDoctorResponse>> GetTimeslotsForDoctorsAsync(List<long> doctorIds);
    }
}
