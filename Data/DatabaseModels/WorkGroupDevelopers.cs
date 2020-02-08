using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
    class WorkGroupDeveloper
    {
        [Key]
        public int Id { get; set; }
        public WorkGroup WorkGroup { get; set; }
        [ForeignKey("WorkGroup")]
        public int WorkGroupId { get; set; }
        public User Developer { get; set; }
    }
}
