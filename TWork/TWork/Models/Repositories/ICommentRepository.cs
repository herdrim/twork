using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface ICommentRepository
    {
        void AddComment(COMMENT comment);
        void RemoveComments(IEnumerable<COMMENT> comments);
    }
}
