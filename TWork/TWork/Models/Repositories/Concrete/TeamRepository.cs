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
    }
}
