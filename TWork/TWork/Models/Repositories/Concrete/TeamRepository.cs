using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class TeamRepository : ITeamRepository
    {
        private TWorkDbContext _ctx;

        public TeamRepository (TWorkDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<TEAM> GetAllTeams() 
            => _ctx.TEAMs;

        public TEAM GetTeamById(int teamId) 
            => _ctx.TEAMs.FirstOrDefault(x => x.ID == teamId);

        public TEAM GetTeamByName(string teamName)
            => _ctx.TEAMs.FirstOrDefault(x => x.NAME == teamName);

        public IEnumerable<TEAM> GetTeamsByUser(USER user) 
            => _ctx.USERS_TEAMs.Where(x => x.USER == user).Select(x => x.TEAM);        

        public IEnumerable<TEAM> GetTeamsByUserId(string userId) 
            => _ctx.USERS_TEAMs.Where(x => x.USER_ID == userId).Select(x => x.TEAM);

        public void AssignUserToTeamWithRole(USER_TEAM userTeam, USER_TEAM_ROLES userTeamRole)
        {
            if (_ctx.USERS_TEAMs.FirstOrDefault(x => x.USER == userTeam.USER && x.TEAM == userTeam.TEAM) == null)
            {
                _ctx.USERS_TEAMs.Add(userTeam);
            }
            if (_ctx.USER_TEAM_ROLEs.FirstOrDefault(x => x.ROLE == userTeamRole.ROLE && x.TEAM == userTeamRole.TEAM && x.USER == userTeamRole.USER) == null)
            {
                _ctx.USER_TEAM_ROLEs.Add(userTeamRole);
            }
            _ctx.SaveChanges();
        }

        public void AddTeam(TEAM team, USER_TEAM userTeam, USER_TEAM_ROLES userTeamRole)
        {
            _ctx.TEAMs.Add(team);
            _ctx.USERS_TEAMs.Add(userTeam);
            _ctx.USER_TEAM_ROLEs.Add(userTeamRole);
            _ctx.SaveChanges();
        }

        public void UpdateTeamInfo(TEAM team)
        {
            _ctx.TEAMs.Update(team);
            _ctx.SaveChanges();
        }

        public void DeleteTeamMember(TEAM team, string userId)
        {
            _ctx.USERS_TEAMs.RemoveRange(_ctx.USERS_TEAMs.Where(x => x.TEAM_ID == team.ID && x.USER_ID == userId));
            _ctx.USER_TEAM_ROLEs.RemoveRange(_ctx.USER_TEAM_ROLEs.Where(x => x.TEAM_ID == team.ID && x.USER_ID == userId));
            _ctx.SaveChanges();
        }

        public bool IsTeamMember(USER user, int teamId)
        {
            TEAM team = GetTeamById(teamId);
            if (team.USERS_TEAMs.FirstOrDefault(x => x.USER == user) != null)
                return true;
            else
                return false;
        }
    }
}
