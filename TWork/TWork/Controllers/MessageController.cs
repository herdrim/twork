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
    public class MessageController : Controller
    {
        ITeamService _teamService;
        IUserRepository _userRepository;
        IMessageService _messageService;

        public MessageController(ITeamService teamService, IUserRepository userRepository, IMessageService messageService)
        {
            _teamService = teamService;
            _userRepository = userRepository;
            _messageService = messageService;
        }

        public IActionResult TeamMessages(int teamId)
        {
            IEnumerable<TeamMessageViewModel> model = _teamService.GetTeamMessages(teamId);

            return View(model);
        }

        
        public async Task<IActionResult> Content(int messageId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);

            if (_messageService.CheckAccessToMessage(messageId, user))
            {
                MessageViewModel model = _messageService.GetMessageToRead(messageId);
                return View(model);
            }
            else
            {
                // TO DO PRZEKIEROWANIE DO ERROR PAGE
                throw new Exception();
            }

        }
    }
}