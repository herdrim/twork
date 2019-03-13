using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.Entities
{
    public static class TWorkDbInitializer
    {
        public static void SeedUser(UserManager<USER> userManager)
        {
            if (userManager.FindByEmailAsync("admin@example.com").Result == null)
            {
                USER user = new USER
                {
                    UserName = "admin",
                    Email = "admin@example.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "admin").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedData(TWorkDbContext ctx)
        {
            if (ctx.TEAMs.Count() == 0)
            {
                TEAM team = new TEAM { NAME = "TeamAdmin" };
                TEAM team2 = new TEAM { NAME = "Team2" };
                TEAM team3 = new TEAM { NAME = "TeamHerdrim" };
                TEAM team4 = new TEAM { NAME = "TeamBob" };

                ROLE role = new ROLE
                {
                    NAME = "Leader",
                    DESCRIPTION = "Team leader",
                    IS_REQUIRED = true,
                    IS_TEAM_OWNER = true,
                    CAN_ASSIGN_TASK = true,
                    CAN_COMMENT = true,
                    CAN_CREATE_TASK = true,
                    CAN_MANAGE_USERS = true
                };
                ROLE role2 = new ROLE
                {
                    NAME = "Programmer",
                    DESCRIPTION = "Programmer",
                    IS_REQUIRED = false,
                    IS_TEAM_OWNER = false,
                    CAN_ASSIGN_TASK = false,
                    CAN_COMMENT = true,
                    CAN_CREATE_TASK = false,
                    CAN_MANAGE_USERS = true
                };
                ROLE role3 = new ROLE
                {
                    NAME = "Member",
                    DESCRIPTION = "Member",
                    IS_REQUIRED = false,
                    IS_TEAM_OWNER = false,
                    CAN_ASSIGN_TASK = false,
                    CAN_COMMENT = false,
                    CAN_CREATE_TASK = false,
                    CAN_MANAGE_USERS = false
                };

                USER admin = ctx.Users.FirstOrDefault(x => x.Email == "admin@example.com");
                USER_TEAM userTeam = new USER_TEAM { USER = admin, TEAM = team };
                USER_TEAM userTeam2 = new USER_TEAM { USER = admin, TEAM = team2 };

                USER_TEAM_ROLES userTeamRoles = new USER_TEAM_ROLES { USER = admin, TEAM = team, ROLE = role };
                USER_TEAM_ROLES userTeamRoles2 = new USER_TEAM_ROLES { USER = admin, TEAM = team2, ROLE = role };
                USER_TEAM_ROLES userTeamRoles3 = new USER_TEAM_ROLES { USER = admin, TEAM = team2, ROLE = role2 };

                ctx.TEAMs.AddRange(team, team2, team3, team4);
                ctx.ROLEs.AddRange(role, role2, role3);
                ctx.USERS_TEAMs.AddRange(userTeam, userTeam2);
                ctx.USER_TEAM_ROLEs.AddRange(userTeamRoles, userTeamRoles2, userTeamRoles3);

                ctx.SaveChanges();
            }

            if(ctx.MESSAGE_TYPEs.Count() == 0)
            {
                MESSAGE_TYPE msgType1 = new MESSAGE_TYPE
                {
                    NAME = "TEAM_JOIN_REQUEST"
                };
                MESSAGE_TYPE msgType2 = new MESSAGE_TYPE
                {
                    NAME = "INFO"
                };

                ctx.MESSAGE_TYPEs.AddRange(msgType1, msgType2);

                ctx.SaveChanges();
            }
        }
    }
}
