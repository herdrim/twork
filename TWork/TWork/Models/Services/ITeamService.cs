using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface ITeamService
    {
        UserTeamsRolesViewModel GetUserTeams(USER user);
        List<OtherTeamViewModel> GetOtherTeamsByUser(USER user);
        void SendJoinRequest(int teamId, USER user);
        IEnumerable<TeamMessageViewModel> GetTeamMessages(int teamId);
    }
}
