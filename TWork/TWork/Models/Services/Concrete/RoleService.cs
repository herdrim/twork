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
        IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository, ITeamRepository teamRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
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

        public TeamRoleViewModel GetRolesByTeam(int teamId)
        {            
            TEAM team = _teamRepository.GetTeamById(teamId);
            TeamRoleViewModel teamRoles = new TeamRoleViewModel();
            if (team != null)
            {
                teamRoles.TeamId = team.ID;
                teamRoles.Roles = new List<RoleForTeamViewModel>();
                var roles = _roleRepository.GetRolesByTeam(team);
                if (roles != null)
                {
                    foreach(var role in roles)
                    {
                        int usersCount = _roleRepository.GetUsersByTeamRole(role, team).Count();
                        RoleForTeamViewModel teamRole = new RoleForTeamViewModel
                        {                            
                            RoleId = role.ID,
                            RoleName = role.NAME,
                            UsersInRoleCount = usersCount
                        };
                        teamRoles.Roles.Add(teamRole);
                    }
                }
            }

            return teamRoles;
        }

        public void CreateRole(RoleCreateViewModel roleModel)
        {
            ROLE role = new ROLE
            {
                NAME = roleModel.RoleName,
                DESCRIPTION = roleModel.RoleDescription,
                TEAM_ID = roleModel.TeamId,
                IS_REQUIRED = false,
                IS_TEAM_OWNER = false,
                CAN_ASSIGN_TASK = roleModel.CanAssignTask,
                CAN_COMMENT = roleModel.CanComment,
                CAN_CREATE_TASK = roleModel.CanCreateTask,
                CAN_MANAGE_USERS = roleModel.CanManageUsers
            };

            _roleRepository.AddRole(role);
        }

        public bool IsRoleExist(string roleName, int? exceptRoleId = null)
        {
            ROLE role = _roleRepository.GetRoleByName(roleName);
            if (role != null)
            {
                if (exceptRoleId != null)
                {
                    return role.ID != (exceptRoleId ?? 0);
                }
                return true;
            }
            else
                return false;
        }

        public RoleEditModel GetRoleWithMembersForEdit(int roleId, int teamId)
        {
            ROLE role = _roleRepository.GetRoleById(roleId);
            TEAM team = _teamRepository.GetTeamById(teamId);
            RoleEditModel roleModel = new RoleEditModel();
            if (role != null && team != null)
            {
                roleModel = new RoleEditModel()
                {
                    TeamId = team.ID,
                    RoleId = role.ID,
                    IsDefaultRole = role.TEAM_ID == null ? true : false,
                    RoleName = role.NAME,
                    RoleDescription = role.DESCRIPTION,
                    CanAssignTask = role.CAN_ASSIGN_TASK,
                    CanComment = role.CAN_COMMENT,
                    CanCreateTask = role.CAN_CREATE_TASK,
                    CanManageUsers = role.CAN_MANAGE_USERS,
                    RoleMembers = new List<RoleEditMemberModel>(),
                    OtherTeamMembers = new List<RoleEditMemberModel>()
                };

                var roleMembers = _roleRepository.GetUsersByTeamRole(role, team);
                var teamMembers = _userRepository.GetUsersByTeam(team);
                if (teamMembers != null)
                {
                    if (roleMembers != null)
                    {
                        teamMembers = teamMembers.Except(roleMembers);
                        roleModel.RoleMembers = roleMembers.Select(x => new RoleEditMemberModel { MemberName = x.UserName, MemberId = x.Id, IsMember = true });
                    }
                    roleModel.OtherTeamMembers = teamMembers.Select(x => new RoleEditMemberModel { MemberName = x.UserName, MemberId = x.Id });
                }
            }

            return roleModel;
        }

        public void SaveEditedRole(RoleEditModel editModel)
        {
            TEAM team = _teamRepository.GetTeamById(editModel.TeamId);
            if (team != null)
            {
                if (!editModel.IsDefaultRole)
                {
                    ROLE role = _roleRepository.GetRoleById(editModel.RoleId);
                    role.NAME = editModel.RoleName;
                    role.DESCRIPTION = editModel.RoleDescription;
                    role.CAN_ASSIGN_TASK = editModel.CanAssignTask;
                    role.CAN_COMMENT = editModel.CanComment;
                    role.CAN_CREATE_TASK = editModel.CanCreateTask;
                    role.CAN_MANAGE_USERS = editModel.CanManageUsers;
                    _roleRepository.UpdateRole(role);
                }

                if (editModel.OtherTeamMembers != null)
                {
                    IEnumerable<UserTeamRoleModel> usersToAdd = editModel.OtherTeamMembers
                            .Where(x => x.IsMember)
                            .Select(x => new UserTeamRoleModel { UserId = x.MemberId, RoleId = editModel.RoleId, TeamId = team.ID });
                    AddUsersToRole(usersToAdd);
                }
                if (editModel.RoleMembers != null)
                {
                    IEnumerable<UserTeamRoleModel> usersToDelete =
                        editModel.RoleMembers
                            .Where(x => !x.IsMember)
                            .Select(x => new UserTeamRoleModel { UserId = x.MemberId, RoleId = editModel.RoleId, TeamId = team.ID });
                    RemoveUsersFromRole(usersToDelete);
                }
            }
        }

        private void AddUsersToRole(IEnumerable<UserTeamRoleModel> userTeamRoleModel)
        {
            IEnumerable<USER_TEAM_ROLES> userTeamRoles = userTeamRoleModel.Select(x => new USER_TEAM_ROLES { USER_ID = x.UserId, ROLE_ID = x.RoleId, TEAM_ID = x.TeamId });
            _roleRepository.AddUserTeamRoles(userTeamRoles);
        }

        private void RemoveUsersFromRole(IEnumerable<UserTeamRoleModel> userTeamRoleModel)
        {
            IEnumerable<USER_TEAM_ROLES> userTeamRoles = userTeamRoleModel.Select(x => new USER_TEAM_ROLES { USER_ID = x.UserId, ROLE_ID = x.RoleId, TEAM_ID = x.TeamId });
            _roleRepository.RemoveUserTeamRoles(userTeamRoles);
        }
    }
}
