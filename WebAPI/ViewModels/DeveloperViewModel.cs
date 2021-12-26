using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels
{
    public class DeveloperViewModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(35)]
        public string name { get; set; }
        [Required]
        public int projectId { get; set; }
        //public Project project { get; set; }
        public DateTimeOffset addedDate { get; set; }
        public decimal costByDay { get; set; }

    }
}
