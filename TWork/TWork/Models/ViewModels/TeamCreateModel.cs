using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TeamCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
