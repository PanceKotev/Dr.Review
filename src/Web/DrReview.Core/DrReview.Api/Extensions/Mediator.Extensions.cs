namespace DrReview.Api.Extensions
{
    using DrReview.Common.Query;
    using MediatR;

    public static partial class Extensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Program),
                typeof(GetDoctorsBySearchwordQueryHandler));
            return services;
        }
    }
}
