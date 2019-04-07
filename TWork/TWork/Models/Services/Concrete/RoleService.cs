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
                            UsersInRoleCount = usersCount,
                            IsDefaultRole = role.TEAM == null ? true : false
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

        public RoleEditModel GetRoleForEdit(int roleId, int teamId)
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
                    CanManageUsers = role.CAN_MANAGE_USERS
                };
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
            }
        }

        public void DeleteRole(int roleId, int teamId)
        {
            ROLE role = _roleRepository.GetRoleById(roleId);
            TEAM team = _teamRepository.GetTeamById(teamId);

            if (team != null && role != null && role.TEAM != null && role.TEAM == team)
            {
                ROLE basicRole = _roleRepository.GetBasicRole();
                var usersInRole = _roleRepository.GetUsersByTeamRole(role, team);
                List<USER_TEAM_ROLES> userTeamRolesToUpdate = new List<USER_TEAM_ROLES>();
                List<USER_TEAM_ROLES> userTeamRolesToDelete = new List<USER_TEAM_ROLES>();
                foreach (var userInRole in usersInRole)
                {
                    bool deleteUserRole = false;
                    if (userInRole.USER_TEAM_ROLEs.Where(x => x.ROLE != role && x.TEAM == team).FirstOrDefault() != null)
                        deleteUserRole = true;

                    List<USER_TEAM_ROLES> userRoles = userInRole.USER_TEAM_ROLEs.Where(x => x.ROLE == role && x.TEAM == team).ToList();
                    if (!deleteUserRole)
                    {
                        foreach (var ur in userRoles)
                        {
                            ur.ROLE = basicRole;
                        }
                        userTeamRolesToUpdate.AddRange(userRoles);
                    }
                    else
                        userTeamRolesToDelete.AddRange(userRoles);
                }
                _roleRepository.UpdateUserTeamRolesRange(userTeamRolesToUpdate);
                _roleRepository.RemoveUserTeamRoles(userTeamRolesToDelete);

                _roleRepository.RemoveRole(role);
            }
        }

        public RoleAssignViewModel GetMembersToAssign(int teamId, int roleId)
        {
            TEAM team = _teamRepository.GetTeamById(teamId);
            ROLE role = _roleRepository.GetRoleById(roleId);
            
            if (team != null && role != null)
            {                
                var roleMembers = _roleRepository.GetUsersByTeamRole(role, team);
                var teamOtherMembers = _userRepository.GetUsersByTeam(team).Except(roleMembers);
                RoleAssignViewModel roleAssignModel = new RoleAssignViewModel
                {
                    RoleId = role.ID,
                    RoleName = role.NAME,
                    TeamId = team.ID,
                    RoleMembers = roleMembers.Select(x => new RoleAssignMemberModel { MemberId = x.Id, MemberName = x.UserName }),
                    OtherTeamMembers = teamOtherMembers.Select(x => new RoleAssignMemberModel { MemberId = x.Id, MemberName = x.UserName })
                };

                return roleAssignModel;
            }

            return null;
        }

        public async Task<bool> AssignUsersToRoleAsync(RoleAssignInputModel roleAssignModel)
        {
            TEAM team = _teamRepository.GetTeamById(roleAssignModel.TeamId);
            ROLE role = _roleRepository.GetRoleById(roleAssignModel.RoleId);
            bool canDelete = false;

            if (team != null && role != null)
            {
                canDelete = true;

                List<USER_TEAM_ROLES> usersToRemove = new List<USER_TEAM_ROLES>();
                if (roleAssignModel.IdsToRemove != null)
                {                    
                    foreach (string idToRemove in roleAssignModel.IdsToRemove)
                    {
                        USER user = await _userRepository.GetUserById(idToRemove);
                        if (user != null && _teamRepository.IsTeamMember(user, team.ID))
                        {
                            usersToRemove.Add(user.USER_TEAM_ROLEs.FirstOrDefault(x => x.ROLE == role && x.TEAM == team));
                        }
                    }
                }

                List<USER_TEAM_ROLES> usersToAdd = new List<USER_TEAM_ROLES>();
                if (roleAssignModel.IdsToAdd != null)
                {
                    foreach (string idToAdd in roleAssignModel.IdsToAdd)
                    {
                        USER user = await _userRepository.GetUserById(idToAdd);
                        if (user != null && _teamRepository.IsTeamMember(user, team.ID))
                        {
                            usersToAdd.Add(new USER_TEAM_ROLES { ROLE = role, TEAM = team, USER = user });
                        }
                    }                    
                }
                
                if (role.IS_REQUIRED && usersToAdd.Count <= 0)
                {
                    var userRoleCount = _roleRepository.GetUsersByTeamRole(role, team).Count();
                    canDelete = usersToRemove.Count < userRoleCount;
                }

                if (canDelete)
                    _roleRepository.AddAndDeleteUserTeamRoles(usersToAdd, usersToRemove);
            }

            return canDelete;
        }
    }
}
