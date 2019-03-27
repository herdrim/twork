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
    public class TeamController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        IUserService _userService;
        ITeamRepository _teamRepository;
        IMessageService _messageService;

        public TeamController(ITeamService teamService, IUserRepository userRepository, IUserService userService, ITeamRepository teamRepository, IMessageService messageService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _userService = userService;
            _teamRepository = teamRepository;
            _messageService = messageService;
        }

        // Lista wszystkich zespołów
        public async Task<IActionResult> Index()
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            List<OtherTeamViewModel> model = _teamService.GetOtherTeamsByUser(user);

            return View(model);
        }               

        public IActionResult CreateTeam()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam(TeamCreateModel teamModel)
        {
            bool isError = false;
            if (ModelState.IsValid)
            {
                USER user = await _userRepository.GetUserByContext(HttpContext.User);
                if (!_teamService.CreateTeam(user, teamModel.Name))
                {
                    isError = true;
                    ModelState.AddModelError(nameof(TeamCreateModel.Name), "This name is already in use");
                }
            }
            else
            {
                isError = true;
            }

            if (isError)
                return View(teamModel);
            else
                return RedirectToAction("Index");                
        }

        // Wysyłanie prośby o dołączenie do zespołu
        [HttpPost]
        public async Task<IActionResult> SendTeamJoinRequest(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            _teamService.SendJoinRequest(teamId, user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AcceptInvite(int teamId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            USER sender = null;
            if (_messageService.IsUserInvitedToTeam(user, teamId, out sender))
            {
                bool isAssigned = await _userService.AssignUserToTeamWithBasicRole(user.Id, sender.Id, teamId);
                if (isAssigned)
                {
                    return RedirectToAction("MyMessages", "Message");
                }
            }

            return RedirectToAction("AccessDenied", "Account");
        }
    }
}