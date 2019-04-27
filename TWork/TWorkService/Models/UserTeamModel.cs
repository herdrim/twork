using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWorkService.Models
{
    public class UserTeamModel
    {
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public int TeamId { get; set; }
    }
}
