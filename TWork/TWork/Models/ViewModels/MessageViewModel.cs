using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsReaded { get; set; }
        public DateTime SendDate { get; set; }
        public USER UserFrom { get; set; }
        public MESSAGE_TYPE MessageType { get; set; }
        public TEAM Team { get; set; }
        public COMMENT Comment { get; set; }
        public USER UserTo { get; set; }
    }
}
