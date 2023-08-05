using System.Collections.Generic;
using System.Linq;

namespace MovieCollectionAPI
{
    public class UserService : IUserService
    {
       private static readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            // Generate a unique ID for the new user (you can use an auto-incrementing ID or a GUID)
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;

            _users.Add(user);
        }       

        public User GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllUsers() => _users;
    }
}