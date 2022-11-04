namespace DrReview.Modules.User.Infrastructure.Common.UnitOfWork.Interfaces
{
    using System.Threading.Tasks;
    using DrReview.Modules.User.Infrastructure.User.Repositories.Interfaces;

    public interface IUserUnitOfWork
    {
        public Task SaveAsync();

        public void Dispose();

        public IUserRepository Users { get; }
    }
}
