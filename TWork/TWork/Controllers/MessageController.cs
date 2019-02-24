using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Common;
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
                
        public async Task<IActionResult> MyMessages()
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (user != null)
            {
                IEnumerable<MessageViewModel> model = await _messageService.GetMessagesToUser(user.Id);
                return View(model);
            }
            else
                return RedirectToAction("AccessDenied", "Account");
        }
        
        public async Task<IActionResult> Content(int messageId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);

            if (_messageService.CheckAccessToMessage(messageId, user))
            {
                MessageViewModel model = _messageService.GetMessageToRead(messageId);
                if (model.MessageType.NAME == MessageTypeNames.TEAM_JOIN_REQUEST)
                    return View("MessageJoinRequest", model);
                else
                    return View(model);
            }
            else
            {                
                return RedirectToAction("AccessDenied", "Account");
            }

        }

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            USER user = await _userRepository.GetUserByContext(HttpContext.User);
            if (_messageService.CheckAccessToMessage(messageId, user))
                _messageService.DeleteMessage(messageId);

            return RedirectToAction("MyMessages");
        }
    }
}