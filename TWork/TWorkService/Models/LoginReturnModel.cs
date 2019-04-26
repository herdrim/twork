using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TWorkService.Models
{
    public class LoginReturnModel
    {
        public string UserId { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public bool Error { get; set; }
        public string ErrorMsg { get; set; }
    }
}
