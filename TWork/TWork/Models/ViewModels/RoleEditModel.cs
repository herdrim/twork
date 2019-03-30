using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class RoleEditModel
    {
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public bool IsDefaultRole { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        [Required]
        public bool CanCreateTask { get; set; }
        [Required]
        public bool CanAssignTask { get; set; }
        [Required]
        public bool CanComment { get; set; }
        [Required]
        public bool CanManageUsers { get; set; }
        public IEnumerable<RoleEditMemberModel> RoleMembers { get; set; }
        public IEnumerable<RoleEditMemberModel> OtherTeamMembers { get; set; }

    }

    public class RoleEditMemberModel
    {
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public bool IsMember { get; set; }
    }
}
