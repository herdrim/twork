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

        public IEnumerable<TEAM> GetTeamsByUser(USER user) 
            => _ctx.USERS_TEAMs.Where(x => x.USER == user).Select(x => x.TEAM);        

        public IEnumerable<TEAM> GetTeamsByUserId(string userId) 
            => _ctx.USERS_TEAMs.Where(x => x.USER_ID == userId).Select(x => x.TEAM);

        public async void AddTeam (TEAM team)
        {
            await _ctx.AddAsync(team);
            await _ctx.SaveChangesAsync();
        }

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
    }
}
