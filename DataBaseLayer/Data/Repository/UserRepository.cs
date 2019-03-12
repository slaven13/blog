using DataBaseAccessLayer.Data.Repository.Contracts;
using DataBaseAccessLayer.Data.Repository.GenericRepository;
using DataBaseAccessLayer.Data.DatabaseContext;
using DataBaseAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseAccessLayer.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IRepository<User> userRepository;

        public UserRepository(BlogContext context,IRepository<User> userRepository)
            : base(context)
        {
            this.userRepository = userRepository;
        }

        public string GetUsername(long userId)
        {
            return userRepository.Get(userId).Username;
        }
    }
}
