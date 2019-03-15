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
        

        public void UpdateTasks(IEnumerable<TASK> tasks)
        {
            _ctx.TASKs.UpdateRange(tasks);
            _ctx.SaveChanges();
        }
    }
}
