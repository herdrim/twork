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

        public ROLE GetRoleById(int roleId)
            => _ctx.ROLEs.FirstOrDefault(x => x.ID == roleId);

        public ROLE GetBasicRole()
        {
            return _ctx.ROLEs.FirstOrDefault(x => x.NAME == "Member");
        }

        public ROLE GetRoleByName(string name)
            => _ctx.ROLEs.FirstOrDefault(x => x.NAME == name);

        public IEnumerable<ROLE> GetRolesByTeam(TEAM team)
            => _ctx.ROLEs.Where(x => x.TEAM == team || x.TEAM == null);

        public IEnumerable<USER> GetUsersByTeamRole(ROLE role, TEAM team)
            => _ctx.USER_TEAM_ROLEs.Where(x => x.TEAM == team && x.ROLE == role).Select(x => x.USER);

        public void AddRole(ROLE role)
        {
            _ctx.ROLEs.Add(role);
            _ctx.SaveChanges();
        }

        public void UpdateRole(ROLE role)
        {
            _ctx.ROLEs.Update(role);
            _ctx.SaveChanges();
        }

        public void RemoveRole(ROLE role)
        {
            _ctx.ROLEs.Remove(role);
            _ctx.SaveChanges();
        }

        public void UpdateUserTeamRolesRange(IEnumerable<USER_TEAM_ROLES> userTeamRolesRange)
        {
            _ctx.USER_TEAM_ROLEs.UpdateRange(userTeamRolesRange);
            _ctx.SaveChanges();
        }

        public void AddAndDeleteUserTeamRoles(IEnumerable<USER_TEAM_ROLES> userTeamRolesToAdd, IEnumerable<USER_TEAM_ROLES> userTeamRolesToDelete)
        {
            _ctx.USER_TEAM_ROLEs.AddRange(userTeamRolesToAdd);
            _ctx.USER_TEAM_ROLEs.RemoveRange(userTeamRolesToDelete);
            _ctx.SaveChanges();
        }

        public void RemoveUserTeamRoles(IEnumerable<USER_TEAM_ROLES> userTeamRoles)
        {
            _ctx.USER_TEAM_ROLEs.RemoveRange(userTeamRoles);
            _ctx.SaveChanges();
        }
    }
}
