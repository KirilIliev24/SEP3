using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
   public class UserStory
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [ForeignKey("Project")]
        public string  Name { get; set; }
    }
}
