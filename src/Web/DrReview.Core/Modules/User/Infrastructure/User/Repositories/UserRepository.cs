namespace DrReview.Modules.User.Infrastructure.User.Repositories
{
    using System.Threading.Tasks;
    using DrReview.Common.Infrastructure.Repository;
    using DrReview.Modules.User.Infrastructure.User.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : BaseRepository<Entities.User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public void AddOrUpdateUser(Entities.User user)
        {
            AttachOrUpdate(user, EntityState.Modified);
        }

        public void UpdateUser(Entities.User user)
        {
            AttachOrUpdate(user, EntityState.Modified);
        }

        public async Task<Entities.User?> GetUserByUidAsync(Guid uid)
        {
            return await Query().FirstOrDefaultAsync(x => x.Uid == uid);
        }

        public void AddUser(Entities.User user)
        {
            Insert(user);
        }
    }
}
