using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TaskListViewModel
    {
        public int TeamId { get; set; }
        public bool CanCreateTask{ get; set; }
        public bool CanAssignTask { get; set; }
        public List<TasksByStatusModel> TasksByStatus { get; set; }
    }

    public class TasksByStatusModel
    {
        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; }
        public List<TASK> Tasks { get; set; }
    }
}
