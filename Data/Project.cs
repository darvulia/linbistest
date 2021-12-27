using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class Project
    {

        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(35)]
        public string name { get; set;}
        public bool isActive { get; set; }
        public DateTimeOffset addedDate { get; set; }
        [NotMapped]
        public int effortRequireInDays { get; set; } = 30;
        [NotMapped]
        public double developmentCost { get; set; }

        public ICollection<Developer> Developers { get; set; }
    }
}
