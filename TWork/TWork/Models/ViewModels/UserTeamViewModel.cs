using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class UserTeamViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<RoleViewModel> UserTeamRoles { get; set; }
    }
}
