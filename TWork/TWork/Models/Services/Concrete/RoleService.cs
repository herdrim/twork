using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class RoleService : IRoleService
    {
        IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public UserTeamPermissionsViewModel GetPermissionsForUserTeam(USER user, TEAM team)
        {
            List<ROLE> roles = _roleRepository.GetRolesByUserTeam(user, team);
            UserTeamPermissionsViewModel userTeamPermissions = new UserTeamPermissionsViewModel();

            foreach(ROLE role in roles)
            {
                if (role.CAN_ASSIGN_TASK)
                    userTeamPermissions.CanAssignTasks = true;
                if (role.CAN_COMMENT)
                    userTeamPermissions.CanComment = true;
                if (role.CAN_CREATE_TASK)
                    userTeamPermissions.CanCreateTasks = true;
                if (role.CAN_MANAGE_USERS)
                    userTeamPermissions.CanManageUsers = true;
            }

            return userTeamPermissions;
        }
    }
}
