namespace DrReview.Modules.User.Application.Queries.User
{
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Common.Results;
    using DrReview.Contracts.Dtos;
    using DrReview.Modules.User.Infrastructure.Common.Contexts;
    using DrReview.Modules.User.Infrastructure.User.Entities;
    using Microsoft.EntityFrameworkCore;

    public class GetUserDetailsQuery : IQuery<Result<GetUserDetailsDto>>
    {
        public GetUserDetailsQuery()
        {
        }

    }

    public class GetUserDetailsQueryHandler : IQueryHandler<GetUserDetailsQuery, Result<GetUserDetailsDto>>
    {
        private readonly UserReadOnlyDatabaseContext _databaseContext;

        private readonly ICurrentUser _currentUser;

        public GetUserDetailsQueryHandler(
            UserReadOnlyDatabaseContext databaseContext,
            ICurrentUser currentUser,
            IDrReviewMediatorService mediator)
        {
            _databaseContext = databaseContext;
            _currentUser = currentUser;
        }

        public async Task<Result<GetUserDetailsDto>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            User? user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.Uid == _currentUser.Uid);

            if(user is null)
            {
                return Result.NotFound<GetUserDetailsDto>(ResultCodes.UserNotFound);
            }

            return Result.Ok(new GetUserDetailsDto(user.Suid, user.FirstName, user.LastName, user.EmailAddress));
        }
    }
}
