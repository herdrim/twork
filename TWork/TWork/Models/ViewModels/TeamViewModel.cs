using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembersCount { get; set; }
        public int TaskCount { get; set; }
        public UserTeamPermissionsViewModel Permissions { get; set; }        
    }
}
