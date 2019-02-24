using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Common;
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

            string title = msg.MESSAGE_TYPE.NAME;
            if (msg.USER_FROM != null)
                title += " od użytkownika " + msg.USER_FROM.UserName;

            MessageViewModel msgViewModel = new MessageViewModel()
            {
                Id = msg.ID,
                Text = msg.TEXT,
                IsReaded = msg.IS_READED,
                SendDate = msg.SEND_DATE,
                Title = title,    // DO ZMIANY
                UserFrom = msg.USER_FROM,
                MessageType = msg.MESSAGE_TYPE,
                Team = msg.TEAM,
                Comment = msg.COMMENT,
                UserTo = msg.USER_TO                
            };

            msg.IS_READED = true;
            _messageRepository.UpdateMessage(msg);

            return msgViewModel;
        }

        public async Task<IEnumerable<MessageViewModel>> GetMessagesToUser(string userId)
        {
            USER user = await _userRepository.GetUserById(userId);
            List<MessageViewModel> retMessages = new List<MessageViewModel>();

            if (user != null)
            {
                IEnumerable<MESSAGE> messages = _messageRepository.GetMessagesToUser(user);

                foreach (MESSAGE msg in messages)
                {
                    MessageViewModel message = new MessageViewModel()
                    {
                        Id = msg.ID,
                        Text = msg.TEXT,
                        Comment = msg.COMMENT,
                        IsReaded = msg.IS_READED,
                        MessageType = msg.MESSAGE_TYPE,
                        Team = msg.TEAM,
                        SendDate = msg.SEND_DATE,
                        Title = msg.MESSAGE_TYPE.NAME,
                        UserFrom = msg.USER_FROM,
                        UserTo = msg.USER_TO
                    };
                    retMessages.Add(message);
                }
            }
            return retMessages;
        }

        public async Task RemoveTeamJoinRequestByUserFrom(string userId, int teamId)
        {
            USER user = await _userRepository.GetUserById(userId);
            TEAM team = _teamRepository.GetTeamById(teamId);

            if(user != null && team != null)
            {
                IEnumerable<MESSAGE> messages = _messageRepository.GetTeamJoinRequestByUserFrom(user, team);
                if (messages != null)
                {
                    _messageRepository.DeleteMessages(messages);
                    await CreateNewMessageForUser(user.Id, null, "You have been assigned to " + team.NAME);
                }
            }
        }

        public async Task CreateNewMessageForUser(string userToId, string userFromId, string content, string messageTypeName = null)
        {
            USER userFrom = null;
            MESSAGE_TYPE messageType = null;
            USER userTo = await _userRepository.GetUserById(userToId);

            if (userFromId != null)
                userFrom = await _userRepository.GetUserById(userFromId);
            if (messageTypeName != null)
                messageType = _messageRepository.GetMessageTypeByName(messageTypeName);

            MESSAGE message = new MESSAGE();
            if (userTo != null)
            {
                message.USER_TO = userTo;
                message.TEXT = content;
                message.MESSAGE_TYPE = messageType ?? _messageRepository.GetMessageTypeByName(MessageTypeNames.INFO);
                message.USER_FROM = userFrom;
                message.SEND_DATE = DateTime.Now;
                _messageRepository.AddMessage(message);
            }

        }

        public void DeleteMessage(int messageId)
        {
            MESSAGE message = _messageRepository.GetMessageById(messageId);
            if (message != null)
            {
                _messageRepository.DeleteMessage(message);
            }
        }
    }
}
