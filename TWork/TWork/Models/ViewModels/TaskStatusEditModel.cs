using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TaskStatusEditModel
    {
        public int StatusId { get; set; }
        public int TeamId { get; set; }
        [Required]
        public string StatusName { get; set; }
    }
}
