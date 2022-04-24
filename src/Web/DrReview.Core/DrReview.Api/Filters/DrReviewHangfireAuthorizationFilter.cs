namespace DrReview.Api.Filters
{
    using Hangfire.Annotations;
    using Hangfire.Dashboard;

    public class DrReviewHangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
