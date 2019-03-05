﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface ITeamRepository
    {
        IEnumerable<TEAM> GetAllTeams();
        TEAM GetTeamById(int teamId);
        TEAM GetTeamByName(string teamName);
        IEnumerable<TEAM> GetTeamsByUser(USER user);
        IEnumerable<TEAM> GetTeamsByUserId(string userId);        
        void AddTeam(TEAM team, USER_TEAM userTeam, USER_TEAM_ROLES userTeamRole);
        void AssignUserToTeamWithRole(USER_TEAM userTeam, USER_TEAM_ROLES userTeamRole);
    }
}
