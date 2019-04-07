using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TaskStatusCreateModel
    {
        public int TeamId { get; set; }
        [Required]
        public string StatusName { get; set; }
        public int IdAfter { get; set; }
    }
}
