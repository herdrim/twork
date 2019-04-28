using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;
using TWorkService.Models;

namespace TWorkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        IUserRepository _userRepository;
        ITeamRepository _teamRepository;
        IUserService _userService;
        ITaskService _taskService;

        public TaskController(IUserRepository userRepository, ITeamRepository teamRepository, IUserService userService, ITaskService taskService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _userService = userService;
            _taskService = taskService;
        }

        [HttpPost("GetTasks")]
        public async Task<List<TasksWithStatusModel>> GetTasks([FromBody] UserTeamModel teamModel)
        {
            LoginUserViewModel details = new LoginUserViewModel
            {
                Email = teamModel.UserLogin,
                Password = teamModel.Password
            };

            bool succeeded = await _userService.LoginAsync(details);
            if (succeeded)
            {
                USER user = await _userRepository.GetUserByEmail(details.Email);
                if (_teamRepository.IsTeamMember(user, teamModel.TeamId))
                {
                    var model = _taskService.TaskList(teamModel.TeamId, user);                    
                    if (model.TasksByStatus != null)
                    {
                        List<TasksWithStatusModel> retModel = new List<TasksWithStatusModel>();
                        foreach (var taskByStatus in model.TasksByStatus)
                        {
                            TasksWithStatusModel tasksWithStatus = new TasksWithStatusModel
                            {
                                TaskStatusId = taskByStatus.TaskStatusId,
                                TaskStatusName = taskByStatus.TaskStatusName,
                                Tasks = taskByStatus.Tasks.Select(x => new TaskModel
                                {
                                    TaskId = x.ID,
                                    TaskDescription = x.DESCRIPTION,
                                    TaskTitle = x.TITLE,
                                    Deathline = x.DEATHLINE.ToString(),
                                    StartTime = x.START_TIME.ToString(),
                                    EndTime = x.END_TIME.ToString()
                                }).ToList()
                            };
                            retModel.Add(tasksWithStatus);
                        }
                        return retModel;
                    }                    
                }
            }
            return null;
        }

        [HttpPost("ChangeTaskStatus")]
        public async Task<bool> ChangeTaskStatus([FromBody] ChangeTaskStatusModel taskStatusModel)
        {
            bool isStatusChanged = false;
            LoginUserViewModel details = new LoginUserViewModel
            {
                Email = taskStatusModel.UserLogin,
                Password = taskStatusModel.Password
            };

            bool succeeded = await _userService.LoginAsync(details);            
            if (succeeded)
            {
                USER user = await _userRepository.GetUserByEmail(details.Email);
                if (_teamRepository.IsTeamMember(user, taskStatusModel.TeamId))
                {
                    isStatusChanged = _taskService.ChangeTaskStatus(user, taskStatusModel.TaskId, taskStatusModel.TaskStatusId, taskStatusModel.TeamId);
                }
            }

            return isStatusChanged;
        }
    }
}