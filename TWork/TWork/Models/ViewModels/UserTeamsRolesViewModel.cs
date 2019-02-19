using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class UserTeamsRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserTeamViewModel> UserTeams { get; set; }
    }
}
