using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;

        public HomeController(ITeamService teamService, IUserRepository userRepository)
        {
            _teamService = teamService;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            UserTeamsRolesViewModel model = _teamService.GetUserTeams(user);

            return View(model);
        }
    }
}