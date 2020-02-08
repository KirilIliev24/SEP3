using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
    public class WorkGroup
    {
        [Key]
        public int WorkGroupId { get; set; }
        public User Creator { get; set; }
        public User ScrumMaster { get; set; }
        public User ProductOwner { get; set; }
        [ForeignKey("Project")]
        public string Name { get; set; }
        public List<User> Developers = new List<User>();
        public Project Project { get; set; }
       public WorkGroup(string projectname,User creator)
        {
            Name = projectname;
            Creator = creator;
        }
        public WorkGroup(string projectname)
        {
            Name = projectname;
        }
        public WorkGroup()
        {
        }
    }
}
