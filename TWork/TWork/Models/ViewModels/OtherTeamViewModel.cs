using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class OtherTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MembersCount { get; set; }
        public bool IsRequestSent { get; set; }
    }
}
