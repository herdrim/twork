using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface ITaskService
    {
        TaskListViewModel TaskList(int teamId, USER user);
        TaskDetailsViewModel TaskDetails(int taskId, int teamId, USER user);
        Task ModifyTask(TaskModificationModel modificationModel, USER user);
        void AddCommentToTask(CommentAddModel commentModel, USER user);
        IEnumerable<TaskCommentViewModel> GetTaskComments(int taskId);
        void CreateTask(TaskCreateModel createModel, USER user);
        void DeleteTask(int taskId, int teamId);
        TaskStatusViewModel GetTaskStatusesForTeam(int teamId);
        TaskStatusEditModel GetTaskStatusForEdit(int statusId, int teamId);
        void UpdateTaskStatus(TaskStatusEditModel statusModel);
        void DeleteTaskStatus(int taskStatusId, int teamId);
        TaskStatusCreateViewModel GetTaskStatusesForCreate(int teamId);
        void CreateTaskStatus(TaskStatusCreateModel createModel);
        IEnumerable<TASK> GetTasksByTeamAndStatusIds(int teamId, int taskStatusId);
        bool ChangeTaskStatus(USER user, int taskId, int newStatusId, int teamId);
        TasksCalendarViewModel TasksForCalendar(USER user, int teamId, DateTime dateFrom, DateTime dateTo, bool userTasksOnly);
    }
}
