using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWork.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        //IMessageService _messageService;
        //IRoleService _roleService;
        ITeamRepository _teamRepository;
        ITaskService _taskService;
        IPermissionService _permissionService;

        public TaskController(ITeamService teamService, IUserRepository userRepository, /*IMessageService messageService, IRoleService roleService,*/ ITeamRepository teamRepository, ITaskService taskService, IPermissionService permissionService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            //_messageService = messageService;
            //_roleService = roleService;
            _teamRepository = teamRepository;
            _taskService = taskService;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                var model = _taskService.TaskList(teamId, user);
                if (model != null)
                    return View(model);
                else
                    return RedirectToAction("Index", "TaskStatus", new { teamId = teamId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> CreateTask(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToCreateTasks(user, teamId))
            {
                return View(new TaskCreateModel { TeamId = teamId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateModel createModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToCreateTasks(user, createModel.TeamId))
            {
                if (ModelState.IsValid)
                {
                    _taskService.CreateTask(createModel, user);
                    return RedirectToAction("Index", new { teamId = createModel.TeamId });
                }
                else
                    return View(createModel);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> TasksCalendar(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                DateTime today = DateTime.Now;
                return View(new CalendarDataViewModel
                {
                    TeamId = teamId,
                    DateFrom = new DateTime(today.Year, today.Month, 1),
                    DateTo = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)),
                    OnlyUserTasks = false
                });
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #region AJAX ACTIONS

        public async Task<IActionResult> TaskDetails(int taskId, int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                return PartialView("_TaskModalPartial", _taskService.TaskDetails(taskId, teamId, user));
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task DeleteTask(TaskModificationModel modificationModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToCreateTasks(user, modificationModel.TeamId))
            {
                _taskService.DeleteTask(modificationModel.TaskId, modificationModel.TeamId);
            }
        }

        [HttpPost]
        public async Task SaveTask(TaskModificationModel modificationModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, modificationModel.TeamId))
            {
                await _taskService.ModifyTask(modificationModel, user);
            }
        }

        [HttpPost]
        public async Task SaveComment(CommentAddModel commentModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, commentModel.TeamId) && _permissionService.CheckPermissionToCommentTasks(user, commentModel.TeamId))
            {
                _taskService.AddCommentToTask(commentModel, user);
            }
        }

        public async Task<string> GetComments(int taskId, int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                return JsonConvert.SerializeObject(_taskService.GetTaskComments(taskId));
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ShowTaskCalendar(CalendarDataViewModel model)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, model.TeamId))
            {
                if (ModelState.IsValid)
                {
                    var retModel = _taskService.TasksForCalendar(user, model.TeamId, model.DateFrom, model.DateTo, model.OnlyUserTasks);
                    return PartialView("_TasksCalendarPartial", retModel);
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion
    }
}