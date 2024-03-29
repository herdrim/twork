﻿using System;
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
    public class RoleController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        IUserService _userService;
        IMessageService _messageService;
        IRoleService _roleService;
        IPermissionService _permissionService;

        public RoleController(ITeamService teamService, IUserRepository userRepository, IUserService userService, IMessageService messageService, IRoleService roleService, IPermissionService permissionService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _userService = userService;
            _messageService = messageService;
            _roleService = roleService;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamId))
            {
                return View(_roleService.GetRolesByTeam(teamId));
            }

            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> CreateRole(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamId))
            {
                RoleCreateViewModel model = new RoleCreateViewModel { TeamId = teamId };
                return View(model);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel roleCreateModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, roleCreateModel.TeamId))
            {
                if (ModelState.IsValid)
                {
                    if (!_roleService.IsRoleExist(roleCreateModel.RoleName))
                    {
                        _roleService.CreateRole(roleCreateModel);
                        return RedirectToAction("Index", new { teamId = roleCreateModel.TeamId });
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(roleCreateModel.RoleName), "Role with this name already exist");
                    }
                }
                return View(roleCreateModel);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> EditRole(int teamId, int roleId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamId))
            {
                return View(_roleService.GetRoleForEdit(roleId, teamId));
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditModel editModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, editModel.TeamId))
            {
                if (ModelState.IsValid)
                {
                    if (!_roleService.IsRoleExist(editModel.RoleName, editModel.RoleId))
                    {
                        _roleService.SaveEditedRole(editModel);
                        return RedirectToAction("Index", new { teamId = editModel.TeamId });
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(editModel.RoleName), "Role with this name already exist");
                    }
                }
                return View(editModel);
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        public async Task<IActionResult> AssignRole(int teamId, int roleId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamId))
            {
                RoleAssignViewModel model = _roleService.GetMembersToAssign(teamId, roleId);
                if (model != null)
                    return View(model);
                else
                    return RedirectToAction("Index", new { teamId = teamId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(RoleAssignInputModel roleAssignModel)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, roleAssignModel.TeamId))
            {
                bool canAssign = await _roleService.AssignUsersToRoleAsync(roleAssignModel);
                if (canAssign)
                    return RedirectToAction("Index", new { teamId = roleAssignModel.TeamId });
                else
                {
                    ModelState.AddModelError(string.Empty, "This role is required");
                    return View(_roleService.GetMembersToAssign(roleAssignModel.TeamId, roleAssignModel.RoleId));
                }
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(int roleId, int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_permissionService.CheckPermissionToManageUsers(user, teamId))
            {
                _roleService.DeleteRole(roleId, teamId);
                return RedirectToAction("Index", new { teamId = teamId });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
    }
}