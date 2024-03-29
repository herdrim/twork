﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TWork.Models.Common;
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
        IMessageService _messageService;
        IRoleService _roleService;
        ITeamRepository _teamRepository;
        IPermissionService _permissionService;

        public MyTeamController(ITeamService teamService, IUserRepository userRepository, IUserService userService, IMessageService messageService, IRoleService roleService, ITeamRepository teamRepository, IPermissionService permissionService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _userService = userService;
            _messageService = messageService;
            _roleService = roleService;
            _teamRepository = teamRepository;
            _permissionService = permissionService;
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
            if (_teamRepository.IsTeamMember(user, teamId))
            {
                TeamViewModel model = _teamService.GetUserTeam(user, teamId);
                return View(model);
            }
            else
                return RedirectToAction("AccessDenied", "Account");            
        }

        public async Task<IActionResult> Edit(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckIfUserIsTeamOwner(user, teamId))
            {
                return View(_teamService.GetTeamInformation(teamId));
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamInformationViewModel model)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.GetPermissionsForUserTeam(user, model.TeamId).IsTeamOwner)
            {
                _teamService.SaveTeamInformation(model);
                return RedirectToAction("Details", new { teamId = model.TeamId });
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<string> MyTeams()
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            var teams = _teamService.GetUserTeamsForNavigation(user);
            return JsonConvert.SerializeObject(teams);
        }

        #region JoinRequests
        [HttpPost]
        public async Task<IActionResult> AcceptUserJoinRequest(string userId, int teamToJoinId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamToJoinId))
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
            if (_permissionService.CheckPermissionToManageUsers(user, teamToJoinId))
            {
                await _messageService.RemoveTeamJoinRequestByUserFrom(userId, teamToJoinId);
                return RedirectToAction("TeamMessages", "Message", new { teamId = teamToJoinId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        #endregion
    }
}