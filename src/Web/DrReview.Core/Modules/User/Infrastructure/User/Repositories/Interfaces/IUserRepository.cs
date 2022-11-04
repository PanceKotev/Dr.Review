namespace DrReview.Modules.User.Infrastructure.User.Repositories.Interfaces
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        public void AddOrUpdateUser(Entities.User user);

        public void UpdateUser(Entities.User user);

        public Task<Entities.User?> GetUserByUidAsync(Guid uid);

        public void AddUser(Entities.User user);
    }
}
