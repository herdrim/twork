using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;

namespace TWork.Controllers
{
    public class TaskController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        //IMessageService _messageService;
        //IRoleService _roleService;
        ITeamRepository _teamRepository;
        ITaskService _taskService;

        public TaskController(ITeamService teamService, IUserRepository userRepository, /*IMessageService messageService, IRoleService roleService,*/ ITeamRepository teamRepository, ITaskService taskService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            //_messageService = messageService;
            //_roleService = roleService;
            _teamRepository = teamRepository;
            _taskService = taskService;
        }

        public async Task<IActionResult> Index(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {                
                return View(_taskService.TaskList(teamId, user));
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}