namespace DrReview.Api.HttpClients.MojTermin.Interfaces
{
    using System.Threading.Tasks;

    public interface IMojTerminHttpClient
    {
        Task<HttpResponseMessage> GetDoctorsAsync();
    }
}
