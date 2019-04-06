using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TaskDetailsViewModel
    {
        public int TeamId { get; set; }
        public bool CanAssignTask { get; set; }
        public bool CanComment { get; set; }
        public bool CanCreateTask { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsUserAssignedToTask { get; set; }

        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deathline { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreateTime { get; set; }

        public TASK_STATUS CurrentTaskStatus { get; set; }
        public IEnumerable<TASK_STATUS> TeamTaskStatuses { get; set; }
        public USER User { get; set; }
        public IEnumerable<USER> TeamUsers { get; set; }
        public IEnumerable<COMMENT> Comments { get; set; }
    }
}
