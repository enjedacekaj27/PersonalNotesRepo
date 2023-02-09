using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public
           UserRepository(PersonalNotesDbContext repositoryContext): base(repositoryContext)
        { }


        public User GetUserById(int id)
        {
            var user = base.FindByCondition(x => x.ID == id, false).FirstOrDefault();
            return user;

        }


    }
}
