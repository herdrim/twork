using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class TeamJsonModel
    {
        public int TeamCount { get; set; }
        public int? ActiveTeam { get; set; }
        public List<TeamInfoJsonModel> Teams { get; set; }    
    }

    public class TeamInfoJsonModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
