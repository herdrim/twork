using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWorkService.Models
{
    public class ChangeTaskStatusModel
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public int TeamId { get; set; }
        public int TaskId { get; set; }
        public int TaskStatusId { get; set; }
    }
}
