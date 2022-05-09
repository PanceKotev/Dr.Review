namespace DrReview.Api.HttpClients.MojTermin.Interfaces
{
    using DrReview.Api.HttpClients.MojTermin.Contracts;
    using System.Threading.Tasks;

    public interface IMojTerminHttpClient
    {
        Task<List<long>> GetInstitutionsAsync();

        List<DoctorResponse> GetDoctorsInInstitutions(long[] institutionsIds);
    }
}
