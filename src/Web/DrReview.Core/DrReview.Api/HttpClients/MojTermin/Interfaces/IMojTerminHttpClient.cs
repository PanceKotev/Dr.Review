namespace DrReview.Api.HttpClients.MojTermin.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Contracts.ExternalApi.MojTermin.Responses;

    public interface IMojTerminHttpClient
    {
        Task<List<long>> GetInstitutionsAsync();

        List<DoctorResponse> GetDoctorsInInstitutions(long[] institutionsIds);
    }
}
