using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class UserTeamPermissionsViewModel
    {
        public bool CanCreateTasks { get; set; }
        public bool CanAssignTasks { get; set; }
        public bool CanComment { get; set; }
        public bool CanManageUsers { get; set; }
    }
}
