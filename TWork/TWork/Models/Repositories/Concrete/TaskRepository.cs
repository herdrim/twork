using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class TaskRepository : ITaskRepository
    {
        private TWorkDbContext _ctx;

        public TaskRepository(TWorkDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<TASK> GetTasksByUserTeam(string userId, int teamId)
            => _ctx.TASKs.Where(x => x.TEAM_ID == teamId && x.USER_ID == userId);

        public IEnumerable<TASK> GetTasksByTeamAndStatus(TEAM team, TASK_STATUS status)
            => _ctx.TASKs.Where(x => x.TEAM_ID == team.ID && x.TASK_STATUS == status);

        public IEnumerable<TASK_STATUS> GetTaskStatusesByTeam(TEAM team)
            => _ctx.TASK_STATUSes.Where(x => x.TEAM == team);
        

        public void UpdateTasks(IEnumerable<TASK> tasks)
        {
            _ctx.TASKs.UpdateRange(tasks);
            _ctx.SaveChanges();
        }
    }
}
