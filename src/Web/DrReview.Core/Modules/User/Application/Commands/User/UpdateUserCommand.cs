namespace DrReview.Modules.User.Application.Commands.User
{
    using DrReview.Common.Auth.Interface;
    using DrReview.Common.Mediator.Contracts;
    using DrReview.Common.Results;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UpdateUserCommand : ICommand<Result<EmptyValue>>
    {
        public UpdateUserCommand(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }

    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<EmptyValue>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        private readonly ICurrentUser _currentUser;

        public UpdateUserCommandHandler(IUserUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<EmptyValue>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Infrastructure.User.Entities.User? user = await _unitOfWork.Users.GetUserByUidAsync(_currentUser.Uid);

            if (user is null)
            {
                return Result.NotFound<EmptyValue>(ResultCodes.UserNotFound);
            }

            Result<Infrastructure.User.Entities.User> updatedUser = user.Update(request.FirstName, request.LastName);

            if (updatedUser.IsFailure)
            {
                return Result.FromError<EmptyValue>(updatedUser);
            }
            _unitOfWork.Users.UpdateUser(updatedUser);

            await _unitOfWork.SaveAsync();

            return Result.Ok<EmptyValue>(new EmptyValue());

        }
    }
}
