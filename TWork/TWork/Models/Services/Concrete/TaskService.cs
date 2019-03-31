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

        public TaskService(IRoleRepository roleRepository, ITeamRepository teamRepository, IUserRepository userRepository, ITaskRepository taskRepository, IPermissionService permissionService)
        {
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _permissionService = permissionService;
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

                var taskStatuses = _taskRepository.GetTaskStatusesByTeam(team);
                foreach(var taskStatus in taskStatuses)
                {
                    TasksByStatusModel tasksByStatus = new TasksByStatusModel
                    {
                        TaskStatusId = taskStatus.ID,
                        TaskStatusName = taskStatus.NAME,
                        Tasks = _taskRepository.GetTasksByTeamAndStatus(team, taskStatus).ToList()
                    };

                    taskListModel.TasksByStatus.Add(tasksByStatus);
                }
                return taskListModel;
            }
            return null;
        }
    }
}
