using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class TaskService : ITaskService
    {
        IRoleRepository _roleRepository;
        ITeamRepository _teamRepository;
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;
        IPermissionService _permissionService;
        ICommentRepository _commentRepository;

        public TaskService(IRoleRepository roleRepository, ITeamRepository teamRepository, IUserRepository userRepository, ITaskRepository taskRepository, IPermissionService permissionService, ICommentRepository commentRepository)
        {
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _permissionService = permissionService;
            _commentRepository = commentRepository;
        }

        public TaskListViewModel TaskList(int teamId, USER user)
        {
            TEAM team = _teamRepository.GetTeamById(teamId);
            if(team != null)
            {
                var permissions = _permissionService.GetPermissionsForUserTeam(user, team.ID);
                TaskListViewModel taskListModel = new TaskListViewModel();
                taskListModel.TeamId = team.ID;                
                taskListModel.CanAssignTask = permissions.CanAssignTasks;
                taskListModel.CanCreateTask = permissions.CanCreateTasks;
                taskListModel.TasksByStatus = new List<TasksByStatusModel>();

                var tmpTaskStatuses = _taskRepository.GetTaskStatusesByTeam(team);

                List<TASK_STATUS> taskStatuses = new List<TASK_STATUS>();
                var firstStatus = tmpTaskStatuses.FirstOrDefault(x => x.PREV_STATUS == null);
                taskStatuses.Add(firstStatus);
                TASK_STATUS lastStatus = firstStatus;
                while(lastStatus.NEXT_STATUS != null)
                {
                    TASK_STATUS currStatus = lastStatus.NEXT_STATUS;
                    taskStatuses.Add(currStatus);
                    lastStatus = currStatus;
                }
                
                foreach(var taskStatus in taskStatuses)
                {
                    TasksByStatusModel tasksByStatus = new TasksByStatusModel
                    {
                        TaskStatusId = taskStatus.ID,
                        TaskStatusName = taskStatus.NAME,
                        Tasks = _taskRepository.GetTasksByTeamAndStatus(team, taskStatus).OrderBy(x => x.DEATHLINE).ToList()
                    };

                    taskListModel.TasksByStatus.Add(tasksByStatus);
                }
                return taskListModel;
            }
            return null;
        }

        public TaskDetailsViewModel TaskDetails (int taskId, int teamId, USER user)
        {
            TASK task = _taskRepository.GetTaskById(taskId);
            TEAM team = _teamRepository.GetTeamById(teamId);
            TaskDetailsViewModel taskDetail = new TaskDetailsViewModel();

            if (task != null && team != null && task.TEAM == team)
            {
                taskDetail.TeamId = team.ID;
                UserTeamPermissionsViewModel permissions = _permissionService.GetPermissionsForUserTeam(user, team.ID);
                taskDetail.CanAssignTask = permissions.CanAssignTasks;
                taskDetail.CanComment = permissions.CanComment;
                taskDetail.CanCreateTask = permissions.CanCreateTasks;
                taskDetail.CurrentUserId = user.Id;
                taskDetail.TaskId = task.ID;
                taskDetail.Title = task.TITLE;
                taskDetail.Description = task.DESCRIPTION;
                taskDetail.Deathline = task.DEATHLINE;
                taskDetail.CreateTime = task.CREATE_TIME;
                taskDetail.StartTime = task.START_TIME;
                taskDetail.EndTime = task.END_TIME;
                taskDetail.User = task.USER;
                taskDetail.CurrentTaskStatus = task.TASK_STATUS;
                taskDetail.Comments = task.COMMENTs;
                taskDetail.IsUserAssignedToTask = (task.USER == user);

                List<USER> teamUsers = new List<USER>();
                teamUsers.AddRange(_userRepository.GetUsersByTeam(team).Except(new List<USER> { task.USER })); 
                if (task.USER != null)
                {
                    teamUsers.Add(new USER { UserName = "[empty]", Id = "" });
                    taskDetail.TeamUsers = teamUsers.Prepend(task.USER);
                }
                else
                {
                    taskDetail.TeamUsers = teamUsers.Prepend(new USER { UserName = "[empty]", Id = "" });
                }
                

                List<TASK_STATUS> taskStatuses = new List<TASK_STATUS>();
                taskStatuses.AddRange(_taskRepository.GetTaskStatusesByTeam(team).Except(new List<TASK_STATUS> { task.TASK_STATUS }));                
                taskDetail.TeamTaskStatuses = taskStatuses.Prepend(task.TASK_STATUS);
            }

            return taskDetail;
        }

        public async Task ModifyTask(TaskModificationModel modificationModel, USER user)
        {
            TEAM team = _teamRepository.GetTeamById(modificationModel.TeamId);
            TASK task = _taskRepository.GetTaskById(modificationModel.TaskId);

            if (task != null)
            {
                if (_permissionService.CheckPermissionToCreateTasks(user, team.ID))
                {
                    task.TITLE = modificationModel.Title;
                    task.DESCRIPTION = modificationModel.Description;
                    task.DEATHLINE = modificationModel.Deathline;
                    task.START_TIME = modificationModel.StartTime;
                    task.END_TIME = modificationModel.EndTime;
                    task.USER = String.IsNullOrEmpty(modificationModel.AssignedToId) ? null : await _userRepository.GetUserById(modificationModel.AssignedToId);
                    task.TASK_STATUS = _taskRepository.GetTaskStatusById(modificationModel.StatusId);
                }
                else
                {
                    if (_permissionService.CheckPermissionToAssignTasks(user, team.ID))
                        task.USER = String.IsNullOrEmpty(modificationModel.AssignedToId) ? null : await _userRepository.GetUserById(modificationModel.AssignedToId);
                    if (task.USER == user)
                        task.TASK_STATUS = _taskRepository.GetTaskStatusById(modificationModel.StatusId);
                }

                _taskRepository.UpdateTasks(new List<TASK> { task });
            }
        }

        public void AddCommentToTask (CommentAddModel commentModel, USER user)
        {
            TASK task = _taskRepository.GetTaskById(commentModel.TaskId);

            if (task != null)
            {
                COMMENT comment = new COMMENT
                {
                    CONTENT = commentModel.CommentContent,
                    CREATED = DateTime.Now,
                    TASK = task,
                    USER = user
                };

                _commentRepository.AddComment(comment);
            }
        }

        public IEnumerable<TaskCommentViewModel> GetTaskComments(int taskId)
        {
            TASK task = _taskRepository.GetTaskById(taskId);
            if (task != null)
            {
                return task.COMMENTs.Select(x => new TaskCommentViewModel { CommentId = x.ID, Content = x.CONTENT, Created = x.CREATED, UserName = x.USER.UserName }).OrderBy(x => x.Created);
            }
            return null;
        }

        public void CreateTask(TaskCreateModel createModel, USER user)
        {
            TEAM team = _teamRepository.GetTeamById(createModel.TeamId);
            if (team != null)
            {
                TASK task = new TASK
                {
                    TITLE = createModel.Title,
                    TEAM = team,
                    USER = user,
                    CREATE_TIME = DateTime.Now,
                    DEATHLINE = createModel.Deathline,
                    START_TIME = createModel.StartTime,
                    DESCRIPTION = createModel.Description,
                    END_TIME = createModel.EndTime,
                    TASK_STATUS = _taskRepository.GetTaskStatusesByTeam(team).FirstOrDefault(x => x.PREV_STATUS == null)
                };

                _taskRepository.CreateTask(task);
            }
        }

        public void DeleteTask (int taskId, int teamId)
        {
            TASK task = _taskRepository.GetTaskById(taskId);
            TEAM team = _teamRepository.GetTeamById(teamId);

            if (task != null && team != null && task.TEAM == team)
            {
                if (task.COMMENTs != null)
                    _commentRepository.RemoveComments(task.COMMENTs);

                _taskRepository.RemoveTask(task);
            }

        }
    }
}
