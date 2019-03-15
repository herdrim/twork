using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.ViewModels
{
    public class TeamMemberViewModel
    {
        public int TeamId { get; set; }
        public List<MemberViewModel> Members { get; set; }        
    }
}
