using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TASK> GetTasksByUserTeam(string userId, int teamId);
        void UpdateTasks(IEnumerable<TASK> tasks);
        IEnumerable<TASK_STATUS> GetTaskStatusesByTeam(TEAM team);
        IEnumerable<TASK> GetTasksByTeamAndStatus(TEAM team, TASK_STATUS status);
    }
}
