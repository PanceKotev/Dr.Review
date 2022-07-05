#nullable disable
namespace DrReview.Modules.User.Infrastructure.Common.UnitOfWork
{
    using DrReview.Common.Infrastructure.UnitOfWork;
    using DrReview.Common.Mediator.Interfaces;
    using DrReview.Modules.User.Infrastructure.Common.Contexts;
    using DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces;
    using DrReview.Modules.User.Infrastructure.User.Repositories;
    using DrReview.Modules.User.Infrastructure.User.Repositories.Interfaces;

    public class UserUnitOfWork : BaseUnitOfWork, IUserUnitOfWork
    {
        public UserUnitOfWork(UserDatabaseContext databaseContext, IDrReviewMediatorService mediatorService) :
            base(databaseContext, mediatorService)
        {
        }

        private IUserRepository _userRepository;
        public IUserRepository Users
        {
            get { 
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(DatabaseContext);
                }

                return _userRepository;
            }
        }

    }
}
