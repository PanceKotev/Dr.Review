namespace DrReview.Api.Extensions
{
    using DrReview.Common.Query;
    using DrReview.Modules.Review.Application.Commands;
    using DrReview.Modules.ScheduleNotifications.Application.Commands;
    using DrReview.Modules.User.Application.Commands.User;
    using MediatR;

    public static partial class Extensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Program),
                typeof(GetDoctorsBySearchwordQueryHandler),
                typeof(CreateReviewCommandHandler),
                typeof(SubscribeToMultipleSchedulesCommandHandler),
                typeof(CreateUserIfNotExistsHandler));

            return services;
        }
    }
}
