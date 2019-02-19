using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TeamMessageViewModel
    {
        public int MessageId { get; set; }
        public string Title { get; set; }
        public bool IsReaded { get; set; }
        public DateTime SendDate { get; set; }
        public string MessageTypeName { get; set; }
        public USER Sender { get; set; }
    }
}
