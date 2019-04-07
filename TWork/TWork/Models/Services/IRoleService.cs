using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface IRoleService
    {        
        TeamRoleViewModel GetRolesByTeam(int teamId);
        void CreateRole(RoleCreateViewModel roleModel);
        bool IsRoleExist(string roleName, int? exceptRoleId = null);
        RoleEditModel GetRoleForEdit(int roleId, int teamId);
        void SaveEditedRole(RoleEditModel editModel);
        RoleAssignViewModel GetMembersToAssign(int teamId, int roleId);
        Task<bool> AssignUsersToRoleAsync(RoleAssignInputModel roleAssignModel);
        void DeleteRole(int roleId, int teamId);
    }
}
