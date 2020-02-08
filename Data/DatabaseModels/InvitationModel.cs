using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DatabaseModels
{
    public class InvitationModel
    {
        [Key]
        public  int Id { get; set; }
        [ForeignKey("Project")]
        public string Name { get; set; }
        [ForeignKey("User")]
        public string username { get; set; }
        public Project Project { get; set; }
        public User User{ get; set; }
    }
}
