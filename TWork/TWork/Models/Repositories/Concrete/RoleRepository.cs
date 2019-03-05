using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private TWorkDbContext _ctx;

        public RoleRepository(TWorkDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<ROLE> GetRolesByUserTeam(USER user, TEAM team)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (team == null)
                throw new ArgumentNullException("team");

            var userTeamRoles = _ctx.USER_TEAM_ROLEs.Where(x => x.USER == user && x.TEAM == team);

            List<ROLE> roles = new List<ROLE>();
            foreach (var userTeamRole in userTeamRoles)
            {
                ROLE role = userTeamRole.ROLE;
                if(role != null)
                    roles.Add(role);
            }

            return roles;
        }

        public ROLE GetBasicRole()
        {
            return _ctx.ROLEs.FirstOrDefault(x => x.NAME == "Member");
        }

        public ROLE GetRoleByName(string name)
            => _ctx.ROLEs.FirstOrDefault(x => x.NAME == name);
        
    }
}
