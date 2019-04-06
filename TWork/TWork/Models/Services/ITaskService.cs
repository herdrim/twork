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
    }
}
