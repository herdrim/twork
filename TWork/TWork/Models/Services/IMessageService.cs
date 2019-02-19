using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface IMessageService
    {
        bool CheckAccessToMessage(int messageId, USER user);
        MessageViewModel GetMessageToRead(int messageId);
    }
}
