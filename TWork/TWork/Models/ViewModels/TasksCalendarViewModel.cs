using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TasksCalendarViewModel
    {
        public int TeamId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<CalendarYear> Dates { get; set; }
        public List<TaskForCalendarModel> Tasks { get; set; }
    }

    public class TaskForCalendarModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? Deathline { get; set; }        
    }

    public class CalendarYear
    {
        public int Year { get; set; }
        public List<CalendarMonth> Months { get; set; }
    }

    public class CalendarMonth
    {
        public int Month { get; set; }
        public List<int> Days { get; set; }
    }
}
