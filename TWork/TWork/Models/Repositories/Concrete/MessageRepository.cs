using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class MessageRepository : IMessageRepository
    {
        private TWorkDbContext _ctx;

        public MessageRepository(TWorkDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<MESSAGE> GetAllMessages() 
            => _ctx.MESSAGEs;

        public IEnumerable<MESSAGE_TYPE> GetAllMessageTypes()
            => _ctx.MESSAGE_TYPEs;

        public IEnumerable<MESSAGE> GetMessagesByTeam(TEAM team)
            => _ctx.MESSAGEs.Where(x => x.TEAM == team);

        public IEnumerable<MESSAGE> GetMessagesFromUser(USER user, string messageTypeName = null)
        {
            IEnumerable<MESSAGE> retMessages = null;
            if (!String.IsNullOrEmpty(messageTypeName))
                retMessages =_ctx.MESSAGEs.Where(x => x.USER_FROM == user);
            else
                retMessages = _ctx.MESSAGEs.Where(x => x.USER_FROM == user && x.MESSAGE_TYPE.NAME == messageTypeName);

            return retMessages;
        }

        public IEnumerable<MESSAGE> GetMessagesToUser(USER user, string messageTypeName = null)
        {
            IEnumerable<MESSAGE> retMessages = null;
            if (!String.IsNullOrEmpty(messageTypeName))
                retMessages = _ctx.MESSAGEs.Where(x => x.USER_TO == user);
            else
                retMessages = _ctx.MESSAGEs.Where(x => x.USER_TO == user && x.MESSAGE_TYPE.NAME == messageTypeName);

            return retMessages;
        }

        public MESSAGE GetMessageById(int messageId)
            => _ctx.MESSAGEs.FirstOrDefault(x => x.ID == messageId);

        public MESSAGE_TYPE GetMessageTypeByName(string msgTypeName)
            => _ctx.MESSAGE_TYPEs.FirstOrDefault(x => x.NAME == msgTypeName);


        public void AddMessage(MESSAGE message)
        {
            _ctx.MESSAGEs.Add(message);
            _ctx.SaveChanges();
        }

        public void UpdateMessage(MESSAGE message)
        {
            _ctx.MESSAGEs.Update(message);
            _ctx.SaveChanges();
        }
    }
}
