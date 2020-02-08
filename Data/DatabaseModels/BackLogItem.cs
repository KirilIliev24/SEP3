using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
    public class BackLogItem
    {
        [Key]
        public int Id{ get; set; }
        [ForeignKey("Sprint")]
        public int? SprintId { get; set; }
        [ForeignKey("Project")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public User WorksOnIt { get; set; }
        public int EstimatedTime { get; set; }
        public int ActualTime { get; set; }
    }
}
