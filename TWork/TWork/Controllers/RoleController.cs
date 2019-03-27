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
    public class RoleController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        IUserService _userService;
        IMessageService _messageService;
        IRoleService _roleService;

        public RoleController(ITeamService teamService, IUserRepository userRepository, IUserService userService, IMessageService messageService, IRoleService roleService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _userService = userService;
            _messageService = messageService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index(int teamId, string memberId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamId))
            {
                return View(_roleService.GetRolesByTeam(teamId));
            }

            return RedirectToAction("AccessDenied", "Account");
        }
    }
}