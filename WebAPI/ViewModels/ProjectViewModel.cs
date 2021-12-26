using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class ProjectViewModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(35)]
        public string name { get; set; }
        public bool isActive { get; set; }
        public DateTimeOffset addedDate { get; set; }

        public int effortRequireInDays { get; set; }

        public double developmentCost { get; set; }
    
    
    }
}
