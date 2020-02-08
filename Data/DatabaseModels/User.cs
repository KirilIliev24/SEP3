using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DatabaseModels
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        [Key]
        [MaxLength(20)]
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public User()
        {
        }
        public User(String username, String password)
        {
            firstName = "not_given";
            lastName = "not_given";
            this.username = username;
            email = "not_given";
            this.password = password;
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




