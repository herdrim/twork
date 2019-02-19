using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class TeamService : ITeamService
    {
        ITeamRepository _teamRepository;
        IRoleRepository _roleRepository;
        IMessageRepository _messageRepository;

        public TeamService(ITeamRepository teamRepository, IRoleRepository roleRepository, IMessageRepository messageRepository)
        {
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
            _messageRepository = messageRepository;
        }

        public List<OtherTeamViewModel> GetOtherTeamsByUser(USER user)
        {
            List<OtherTeamViewModel> retTeams = new List<OtherTeamViewModel>();

            IEnumerable<TEAM> allTeams = _teamRepository.GetAllTeams();
            IEnumerable<TEAM> userTeams =_teamRepository.GetTeamsByUser(user);
            IEnumerable<TEAM> teamsRequestSent = _messageRepository.GetMessagesFromUser(user, MessageTypeNames.TEAM_JOIN_REQUEST).Select(x => x.TEAM);
            IEnumerable<TEAM> teams = allTeams.Except(userTeams);

            foreach (TEAM team in teams)
            {
                retTeams.Add(new OtherTeamViewModel
                {
                    Id = team.ID,
                    Name = team.NAME,
                    MembersCount = team.USERS_TEAMs.Count,
                    IsRequestSent = teamsRequestSent.Contains(team)
                });
            }

            return retTeams;
        }

        public UserTeamsRolesViewModel GetUserTeams(USER user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            IEnumerable<TEAM> teams = _teamRepository.GetTeamsByUser(user);
            UserTeamsRolesViewModel userTeamsRoles = new UserTeamsRolesViewModel();
            userTeamsRoles.UserId = user.Id;
            userTeamsRoles.UserName = user.UserName;

            if (teams != null)
            {
                userTeamsRoles.UserTeams = new List<UserTeamViewModel>();                
                foreach (TEAM team in teams)
                {
                    UserTeamViewModel userTeam = new UserTeamViewModel();
                    userTeam.TeamId = team.ID;
                    userTeam.TeamName = team.NAME;
                    List<RoleViewModel> rolesViewModel = new List<RoleViewModel>();
                    var roles = _roleRepository.GetRolesByUserTeam(user, team);
                    foreach (var role in roles)
                    {
                        RoleViewModel roleViewModel = new RoleViewModel();
                        roleViewModel.RoleId = role.ID;
                        roleViewModel.RoleName = role.NAME;
                        roleViewModel.RoleDescription = role.DESCRIPTION;
                        rolesViewModel.Add(roleViewModel);
                    }
                    userTeam.UserTeamRoles = rolesViewModel;
                    userTeamsRoles.UserTeams.Add(userTeam);
                }
            }

            return userTeamsRoles;
        }

        public void SendJoinRequest(int teamId, USER user)
        {
            string msgContent = "Użytkownik " + user.Email + " poprosił o dołączenie go do zespołu.</br><a href='#'>Akceptuj</a>&ensp;<a href='#'>Odrzuć</a>";
            MESSAGE joinRequest = new MESSAGE
            {
                MESSAGE_TYPE = _messageRepository.GetMessageTypeByName(MessageTypeNames.TEAM_JOIN_REQUEST),
                TEAM = _teamRepository.GetTeamById(teamId),
                USER_FROM = user,
                TEXT = msgContent,
                SEND_DATE = DateTime.Now
            };

            _messageRepository.AddMessage(joinRequest);            
        }

        public IEnumerable<TeamMessageViewModel> GetTeamMessages(int teamId)
        {
            List<TeamMessageViewModel> teamMessages = new List<TeamMessageViewModel>();
            TEAM team = _teamRepository.GetTeamById(teamId);
            IEnumerable<MESSAGE> messages = _messageRepository.GetMessagesByTeam(team);
            
            foreach(MESSAGE msg in messages)
            {
                teamMessages.Add(new TeamMessageViewModel
                {
                    MessageId = msg.ID,
                    MessageTypeName = msg.MESSAGE_TYPE.NAME,
                    IsReaded = msg.IS_READED,
                    SendDate = msg.SEND_DATE,
                    Sender = msg.USER_FROM,
                    Title = MessageTypeNames.TEAM_JOIN_REQUEST + " od użytkownika " + msg.USER_FROM.UserName // DO ZMIANY
                });
            }

            return teamMessages;
        }
    }
}
