namespace DrReview.Modules.User.Application.Commands.User
{
    using System.Threading;
    using System.Threading.Tasks;
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.User.Infrastructure.User.Entities;

    public class CreateUserIfNotExistsCommand : ICommand<Result<EmptyValue>>
    {
        public CreateUserIfNotExistsCommand()
        {
        }
    }

    public class CreateUserIfNotExistsHandler : ICommandHandler<CreateUserIfNotExistsCommand, Result<EmptyValue>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        private readonly ICurrentUser _currentUser;

        public CreateUserIfNotExistsHandler(IUserUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<EmptyValue>> Handle(CreateUserIfNotExistsCommand request, CancellationToken cancellationToken)
        {
            User? user = await _unitOfWork.Users.GetUserByUidAsync(_currentUser.Uid);

            if (user is null)
            {
                _unitOfWork.Users.AddUser(User.Create(
                    _currentUser.Uid,
                    _currentUser.FirstName,
                    _currentUser.LastName,
                    _currentUser.Email));
            }

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(new EmptyValue());

        }
    }
}
