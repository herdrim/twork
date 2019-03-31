using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface IRoleRepository
    {
        ROLE GetRoleById(int roleId);
        List<ROLE> GetRolesByUserTeam(USER user, TEAM team);
        ROLE GetRoleByName(string name);
        ROLE GetBasicRole();
        IEnumerable<ROLE> GetRolesByTeam(TEAM team);
        IEnumerable<USER> GetUsersByTeamRole(ROLE role, TEAM team);
        void AddRole(ROLE role);
        void UpdateRole(ROLE role);
        void AddAndDeleteUserTeamRoles(IEnumerable<USER_TEAM_ROLES> userTeamRolesToAdd, IEnumerable<USER_TEAM_ROLES> userTeamRolesToDelete);
        void RemoveUserTeamRoles(IEnumerable<USER_TEAM_ROLES> userTeamRoles);
        void UpdateUserTeamRolesRange(IEnumerable<USER_TEAM_ROLES> userTeamRolesRange);
        void RemoveRole(ROLE role);
    }
}
