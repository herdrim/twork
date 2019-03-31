using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class RoleAssignViewModel
    {
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<RoleAssignMemberModel> RoleMembers { get; set; }
        public IEnumerable<RoleAssignMemberModel> OtherTeamMembers { get; set; }
    }

    public class RoleAssignMemberModel
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }
    }

    public class RoleAssignInputModel
    {
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToRemove { get; set; }
    }
}
