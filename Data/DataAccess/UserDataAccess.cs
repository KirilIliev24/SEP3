using Data.Data;
using Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Data.DataAccess
{
    class UserDataAccess
    {
        public void addUser(User user)
        {
            using (var context = new ThemisContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public User retrieveUserByName(String username)
        {
            using (var context = new ThemisContext())
            {
                var user = context.Users.Find(username);
                return user;
            }
        }
        public string[] GetAllUsernames()
        {
            using (var context = new ThemisContext())
            {
                var usernames = context.Users.Select(u => u.username).ToArray();
                return usernames;
            }
        }
    }
}
