using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TWork.Models.ViewModels
{
    public class RegisterUserModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
