using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        TWorkDbContext _ctx;

        public CommentRepository(TWorkDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddComment(COMMENT comment)
        {
            _ctx.COMMENTs.Add(comment);
            _ctx.SaveChanges();
        }

        public void RemoveComments(IEnumerable<COMMENT> comments)
        {
            _ctx.COMMENTs.RemoveRange(comments);
            _ctx.SaveChanges();
        }
    }
}
