using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataBaseAccessLayer.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IRepository<User> userRepository;

        public UserRepository(BlogContext context, IRepository<User> userRepository)
            : base(context)
        {
            this.userRepository = userRepository;
        }

        public string GetUsername(long userId)
        {
            return userRepository.Get(userId).Username;
        }

        public IList<User> GetUsersWithPostsAndComments()
        {
            return userRepository.Get()
                                 .Include(u => u.Comments)
                                 .Include(u => u.Posts)
                                 .ToList();
        }
    }
}
