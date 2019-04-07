using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface IPermissionService
    {
        UserTeamPermissionsViewModel GetPermissionsForUserTeam(USER user, int teamId);
        /// <summary>
        /// Return true if user has permission to manage users in specified team
        /// </summary>
        /// <param name="user"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        bool CheckPermissionToManageUsers(USER user, int teamId);
        bool CheckPermissionToCommentTasks(USER user, int teamId);
        bool CheckPermissionToAssignTasks(USER user, int teamId);
        bool CheckPermissionToCreateTasks(USER user, int teamId);
        bool CheckIfUserIsTeamOwner(USER user, int teamId);
    }
}
