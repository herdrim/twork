using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TaskStatusCreateViewModel
    {
        public int TeamId { get; set; }
        public string StatusName { get; set; }
        public List<TASK_STATUS> StatusesAfterNew { get; set; }
    }
}
