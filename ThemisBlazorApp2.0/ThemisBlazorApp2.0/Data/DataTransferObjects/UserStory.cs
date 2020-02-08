using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class UserStory
    {
        public String Description { get; set; }
        public UserStory(string description)
        {
            this.Description = description;
        }
    }
}
