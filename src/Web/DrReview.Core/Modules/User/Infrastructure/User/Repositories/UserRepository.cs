namespace DrReview.Modules.User.Infrastructure.User.Repositories
{
    using System.Threading.Tasks;
    using DrReview.Common.Infrastructure.Repository;
    using DrReview.Modules.User.Infrastructure.User.Entities;
    using DrReview.Modules.User.Infrastructure.User.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public void AddOrUpdateUser(User user)
        {
            AttachOrUpdate(user, EntityState.Modified);
        }

        public async Task<User?> GetUserByUidAsync(Guid uid)
        {
            return await Query().FirstOrDefaultAsync(x => x.Uid == uid);
        }

        public void AddUser(User user)
        {
            Insert(user);
        }
    }
}
