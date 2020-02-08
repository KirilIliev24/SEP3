using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DatabaseModels
{
    public class Project
    {
        public List<BackLogItem> BackLogItems = new List<BackLogItem>();
        public List<Sprint> Sprints = new List<Sprint>();
        public List<UserStory> UserStories = new List<UserStory>();
        public WorkGroup WorkGroup { get; set; }
        [Key]
        public string Name { get; set; }
        public Project()
        {
        }
        public Project(string name)
        {
            Name = name;
        }
        public Project(string name, WorkGroup workgroup)
        {
            Name = name;
            WorkGroup = workgroup;
        }
    }
}
