namespace DrReview.Api.Extensions
{
    using DrReview.Common.Query;
    using DrReview.Modules.User.Application.Commands.User;
    using MediatR;

    public static partial class Extensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(
                typeof(Program),
                typeof(GetDoctorsBySearchwordQueryHandler),
                typeof(CreateUserIfNotExistsHandler));
            return services;
        }
    }
}
