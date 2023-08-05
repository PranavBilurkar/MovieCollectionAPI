using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieCollectionAPI
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUserByUsername(string username);
        List<User> GetAllUsers();
    }
}