using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TaskStatusViewModel
    {
        public int TeamId { get; set; }
        public List<TaskStatusModel> TaskStatuses { get; set; }
    }

    public class TaskStatusModel
    {
        public int StatusId { get; set; }
        public int? TeamId { get; set; }
        public string StatusName { get; set; }
        public int? PrevStatusId { get; set; }
        public string PrevStatusName { get; set; }
        public int? NextStatusId { get; set; }
        public string NextStatusName { get; set; }
    }
}
