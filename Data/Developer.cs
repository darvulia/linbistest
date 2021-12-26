using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    public class Developer
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(35)]
        public string name { get; set; }
        [Required]
        public int projectId { get; set;}
        public Project project { get; set; }
        public DateTimeOffset addedDate { get; set; }
        [Column(TypeName = "decimal(14,2)")]
        public decimal costByDay {get; set;}
    }
}
