using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class WorkGroup
    {
        public User Creator { get; set; }
        public User ScrumMaster { get; set; }
        public User ProductOwner { get; set; }
        public string Name { get; set; }
        public List<User> Developers { get; set; }
    }
}
