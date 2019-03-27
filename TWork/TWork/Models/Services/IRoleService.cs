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
        UserTeamPermissionsViewModel GetPermissionsForUserTeam(USER user, int teamId);
        List<TeamRoleViewModel> GetRolesByTeam(int teamId);
    }
}
