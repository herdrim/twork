using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class MessageViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsReaded { get; set; }
        public DateTime SendDate { get; set; }
    }
}
