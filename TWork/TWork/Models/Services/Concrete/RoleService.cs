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
        ITeamRepository _teamRepository;

        public RoleService(IRoleRepository roleRepository, ITeamRepository teamRepository)
        {
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
        }

        public UserTeamPermissionsViewModel GetPermissionsForUserTeam(USER user, int teamId)
        {
            TEAM team = _teamRepository.GetTeamById(teamId);
            UserTeamPermissionsViewModel userTeamPermissions = new UserTeamPermissionsViewModel();
            if (team != null)
            {
                List<ROLE> roles = _roleRepository.GetRolesByUserTeam(user, team);               

                foreach (ROLE role in roles)
                {
                    if (role.IS_TEAM_OWNER)                    
                        userTeamPermissions.IsTeamOwner = true;                    
                    if (role.CAN_ASSIGN_TASK)
                        userTeamPermissions.CanAssignTasks = true;
                    if (role.CAN_COMMENT)
                        userTeamPermissions.CanComment = true;
                    if (role.CAN_CREATE_TASK)
                        userTeamPermissions.CanCreateTasks = true;
                    if (role.CAN_MANAGE_USERS)
                        userTeamPermissions.CanManageUsers = true;
                }
            }

            return userTeamPermissions;
        }
    }
}
