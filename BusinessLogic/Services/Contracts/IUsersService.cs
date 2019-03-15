using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IUsersService
    {
        List<User> GetUsers();
        User GetUserFull(long userId);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(long userId);
    }
}
