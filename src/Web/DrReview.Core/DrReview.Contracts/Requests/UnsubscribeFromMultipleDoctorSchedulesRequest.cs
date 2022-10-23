#nullable disable
namespace DrReview.Contracts.Requests
{
    public class UnsubscribeFromMultipleDoctorSchedulesRequest
    {
        public List<string> ScheduleSuids { get; set; }
    }
}
