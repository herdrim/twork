using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class MessageService : IMessageService
    {
        ITeamRepository _teamRepository;
        IUserRepository _userRepository;
        IMessageRepository _messageRepository;

        public MessageService(ITeamRepository teamRepository, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public bool CheckAccessToMessage(int messageId, USER user)
        {
            MESSAGE msg = _messageRepository.GetMessageById(messageId);

            IEnumerable<TEAM> teams = _teamRepository.GetTeamsByUser(user);
                       
            if (teams.Contains(msg.TEAM) || msg.USER_TO == user)
                return true;
            else
                return false;
        }
        
        public MessageViewModel GetMessageToRead(int messageId)
        {
            MESSAGE msg = _messageRepository.GetMessageById(messageId);

            MessageViewModel msgViewModel = new MessageViewModel
            {
                Text = msg.TEXT,
                IsReaded = msg.IS_READED,
                SendDate = msg.SEND_DATE,
                Title = MessageTypeNames.TEAM_JOIN_REQUEST + " od użytkownika " + msg.USER_FROM.UserName    // DO ZMIANY
            };

            msg.IS_READED = true;
            _messageRepository.UpdateMessage(msg);

            return msgViewModel;
        }
    }
}
