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
    public class MyTeamController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        IUserService _userService;
        ITeamRepository _teamRepository;
        IMessageService _messageService;

        public MyTeamController(ITeamService teamService, IUserRepository userRepository, IUserService userService, ITeamRepository teamRepository, IMessageService messageService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _userService = userService;
            _teamRepository = teamRepository;
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            UserTeamsRolesViewModel model = _teamService.GetUserTeams(user);

            return View(model);
        }

        public async Task<IActionResult> Details(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.IsTeamMember(user, teamId))
            {
                TeamViewModel model = _teamService.GetUserTeam(user, teamId);
                return View(model);
            }
            else
                return RedirectToAction("AccessDenied", "Account");            
        }

        [HttpPost]
        public async Task<IActionResult> AcceptUserJoinRequest(string userId, int teamToJoinId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamToJoinId))
            {
                bool isAssigned = await _userService.AssignUserToTeamWithBasicRole(userId, user.Id, teamToJoinId);
                if (isAssigned)
                    return RedirectToAction("TeamMessages", "Message", new { teamId = teamToJoinId });
            }

            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CancelUserJoinRequest(string userId, int teamToJoinId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamToJoinId))
            {
                await _messageService.RemoveTeamJoinRequestByUserFrom(userId, teamToJoinId);
                return RedirectToAction("TeamMessages", "Message", new { teamId = teamToJoinId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}