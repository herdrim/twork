using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWork.Controllers
{
    [Authorize]
    public class TaskStatusController : Controller
    {
        ITaskService _taskService;
        IPermissionService _permissionService;
        ITeamService _teamService;
        IUserRepository _userRepository;

        public TaskStatusController(ITaskService taskService, IPermissionService permissionService, ITeamService teamService, IUserRepository userRepository)
        {
            _taskService = taskService;
            _permissionService = permissionService;
            _teamService = teamService;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index(int teamId, bool deleteError = false)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
            {
                if (deleteError)
                    ViewBag.TaskStatusError = "Task status can not contains tasks before delete";
                return View(_taskService.GetTaskStatusesForTeam(teamId));                
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> EditStatus(int taskStatusId, int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
            {
                var model = _taskService.GetTaskStatusForEdit(taskStatusId, teamId);
                if (model != null)
                    return View(model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(TaskStatusEditModel editModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, editModel.TeamId))
            {
                if (ModelState.IsValid)
                {
                    _taskService.UpdateTaskStatus(editModel);
                    return RedirectToAction("Index", new { teamId = editModel.TeamId });
                }
                else
                {
                    var model = _taskService.GetTaskStatusForEdit(editModel.StatusId, editModel.TeamId);
                    if (model != null)
                        return View(model);
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        //public async Task<IActionResult> EditStatusOrder(int teamId)
        //{
        //    USER user = await _userRepository.GetUserByContext(HttpContext.User);
        //    if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
        //    {
        //        var model = _taskService.GetTaskStatusForEdit(taskStatusId, teamId);
        //        if (model != null)
        //            return View(model);
        //    }
        //    return RedirectToAction("AccessDenied", "Account");
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteStatus(int taskStatusId, int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
            {
                var tasksInStatus = _taskService.GetTasksByTeamAndStatusIds(teamId, taskStatusId);
                if (tasksInStatus == null || tasksInStatus.Count() <= 0)
                {
                    _taskService.DeleteTaskStatus(taskStatusId, teamId);
                    return RedirectToAction("Index", new { teamId = teamId });
                }
                else
                {                    
                    return RedirectToAction("Index", new { teamId = teamId, deleteError = true });
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> CreateStatus(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
            {
                return View(_taskService.GetTaskStatusesForCreate(teamId));
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(TaskStatusCreateModel createModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, createModel.TeamId))
            {
                if (ModelState.IsValid)
                {
                    _taskService.CreateTaskStatus(createModel);
                    return RedirectToAction("Index", new { teamId = createModel.TeamId });
                }
                else
                {
                    return View(_taskService.GetTaskStatusesForCreate(createModel.TeamId));
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}