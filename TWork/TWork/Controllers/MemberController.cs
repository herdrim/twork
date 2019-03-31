using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWork.Controllers
{
    public class MemberController : Controller
    {
        IUserRepository _userRepository;
        ITeamService _teamService;
        ITeamRepository _teamRepository;

        public MemberController(IUserRepository userRepository, ITeamService teamService, ITeamRepository teamRepository)
        {
            _userRepository = userRepository;
            _teamService = teamService;
            _teamRepository = teamRepository;
        }

        public async Task<IActionResult> Index(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                TeamMemberViewModel model = _teamService.GetTeamMembers(teamId);
                return View(model);
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMember(int teamId, string memberId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamId))
            {
                _teamService.RemoveMember(teamId, memberId);
                return RedirectToAction("Index", new { teamId = teamId });
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }


        public async Task<IActionResult> InviteUser(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamId))
            {
                TeamInformationViewModel model = _teamService.GetTeamInformation(teamId);
                return View(model);
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> SendInvitation(int teamId, string email)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_teamService.CheckPermissionToManageUsers(user, teamId))
            {
                if (!String.IsNullOrEmpty(email))
                {
                    await _teamService.InviteUserToTeam(user, teamId, email);
                    return RedirectToAction("Index", new { teamId = teamId });
                }
                else
                {
                    TeamInformationViewModel model = _teamService.GetTeamInformation(teamId);
                    return View(model);
                }
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }
    }
}