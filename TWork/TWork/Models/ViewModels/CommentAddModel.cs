using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class CommentAddModel
    {
        public int TaskId { get; set; }
        public int TeamId { get; set; }
        public string CommentContent { get; set; }
    }
}
