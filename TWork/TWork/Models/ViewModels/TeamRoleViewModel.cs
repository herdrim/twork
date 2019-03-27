using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TeamRoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UsersInRoleCount { get; set; }
    }
}
