using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TaskCreateModel
    {
        public int TeamId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deathline { get; set; }
        public DateTime? StartTime { get; set; }        
        public DateTime? EndTime { get; set; }
    }
}
