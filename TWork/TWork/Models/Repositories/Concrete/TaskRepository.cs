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

        public TASK GetTaskById(int taskId)
            => _ctx.TASKs.FirstOrDefault(x => x.ID == taskId);

        public TASK_STATUS GetTaskStatusById(int taskStatusId)
            => _ctx.TASK_STATUSes.FirstOrDefault(x => x.ID == taskStatusId);

        public void CreateTask(TASK task)
        {
            _ctx.TASKs.Add(task);
            _ctx.SaveChanges();
        }

        public void RemoveTask(TASK task)
        {
            _ctx.TASKs.Remove(task);
            _ctx.SaveChanges();
        }

        public void UpdateTaskStatus(TASK_STATUS taskStatus)
        {
            _ctx.TASK_STATUSes.Update(taskStatus);
            _ctx.SaveChanges();
        }

        public void RemoveTaskStatus(TASK_STATUS taskStatus)
        {
            _ctx.TASK_STATUSes.Remove(taskStatus);
            _ctx.SaveChanges();
        }

        public void CreateTaskStatus(TASK_STATUS taskStatus)
        {
            _ctx.TASK_STATUSes.Add(taskStatus);
            _ctx.SaveChanges();
        }

        public void UpdateTaskStatuses(IEnumerable<TASK_STATUS> taskStatuses)
        {
            _ctx.TASK_STATUSes.UpdateRange(taskStatuses);
            _ctx.SaveChanges();
        }
    }
}
