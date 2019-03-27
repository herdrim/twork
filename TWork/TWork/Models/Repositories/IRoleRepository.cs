using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface IRoleRepository
    {
        List<ROLE> GetRolesByUserTeam(USER user, TEAM team);
        ROLE GetRoleByName(string name);
        ROLE GetBasicRole();
        IEnumerable<ROLE> GetRolesByTeam(TEAM team);
        IEnumerable<USER> GetUsersByTeamRole(ROLE role, TEAM team);
    }
}
