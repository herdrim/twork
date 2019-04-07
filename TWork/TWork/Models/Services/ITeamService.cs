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
        TeamViewModel GetUserTeam(USER user, int teamId);
        List<OtherTeamViewModel> GetOtherTeamsByUser(USER user);
        TeamInformationViewModel GetTeamInformation(int teamId);
        TeamMemberViewModel GetTeamMembers(int teamId);
        string GetUserTeamsJsonForContext(USER user, int? activeTeamId = null);
        /// <summary>
        /// Create a message (team join request)
        /// </summary>
        /// <param name="teamId">The team user wants to join</param>
        /// <param name="user">User sending the request</param>
        /// <returns></returns>
        void SendJoinRequest(int teamId, USER user);
        IEnumerable<TeamMessageViewModel> GetTeamMessages(int teamId);        
        /// <summary>
        /// Return true if name is free
        /// </summary>
        /// <param name="user"></param>
        /// <param name="teamName"></param>
        /// <returns></returns>
        bool CreateTeam(USER user, string teamName);        
        void SaveTeamInformation(TeamInformationViewModel teamInfo);
        void RemoveMember(int teamId, string userId);
        Task InviteUserToTeam(USER sender, int teamId, string email);
        IEnumerable<MyTeamsForNavigationViewModel> GetUserTeamsForNavigation(USER user);
    }
}
