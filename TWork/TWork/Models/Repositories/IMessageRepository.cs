using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<MESSAGE> GetAllMessages();
        IEnumerable<MESSAGE> GetMessagesFromUser(USER user, string messageTypeName = null);
        IEnumerable<MESSAGE> GetMessagesToUser(USER user, string messageTypeName = null);
        IEnumerable<MESSAGE_TYPE> GetAllMessageTypes();
        IEnumerable<MESSAGE> GetMessagesByTeam(TEAM team);
        IEnumerable<MESSAGE> GetTeamJoinRequestByUserFrom(USER user, TEAM team);
        MESSAGE GetMessageById(int messageId);
        MESSAGE_TYPE GetMessageTypeByName(string msgTypeName);
        void AddMessage(MESSAGE message);
        void UpdateMessage(MESSAGE message);        
        void DeleteMessage(MESSAGE message);
        void DeleteMessages(IEnumerable<MESSAGE> messages);
    }
}
