using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemisBlazorApp2._0.Data.DataTransferObjects
{
    public class Project
    {
        public List<BackLogItem> BackLogItems { get; set; }
        public List<UserStory> UserStories { get; set; }
        public List<Sprint> Sprints { get; set; }
        public WorkGroup WorkGroup { get; set; }
        public String Name { get; set; }

        public Project()
        {
            Sprints = new List<Sprint>();
        }
        public Project(String name)
        {
            this.Name = name;
            Sprints = new List<Sprint>();
        }
        public Project(String name, WorkGroup workGroup)
        {
            Name = name;
            WorkGroup = workGroup;
            Sprints = new List<Sprint>();
        }
    }
}
