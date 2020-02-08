using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
    public class Sprint
    {
        [Key]
        public int SprintId { get; set; }
        public int Number { get; set; }
        public List<BackLogItem> BacklogItems = new List<BackLogItem>();
        [ForeignKey("Project")]
        public string Name { get; set; }
    }
}
