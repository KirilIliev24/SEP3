using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class User
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public User()
        {
        }
        public User(String firstName, String lastName, String username, String email, String password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.email = email;
            this.password = password;
        }
    }
}
