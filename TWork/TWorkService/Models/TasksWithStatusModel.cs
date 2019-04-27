using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWorkService.Models
{
    public class TasksWithStatusModel
    {
        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }

    public class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string Deathline { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
