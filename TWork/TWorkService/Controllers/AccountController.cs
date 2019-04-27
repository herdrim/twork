using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;

namespace TWorkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private IUserRepository _userRepository;

        public AccountController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }


        [HttpPost("Login")]
        public async Task<TWorkService.Models.LoginReturnModel> Login([FromBody()] TWorkService.Models.LoginModel loginModel)
        {            
            LoginUserViewModel details = new LoginUserViewModel
            {
                Email = loginModel.UserLogin,
                Password = loginModel.Password
            };

            bool succeeded = await _userService.LoginAsync(details);
            if (succeeded)
            {
                USER user = await _userRepository.GetUserByEmail(details.Email);
                return new TWorkService.Models.LoginReturnModel
                {
                    UserId = user.Id,
                    UserLogin = user.Email,
                    Password = loginModel.Password,
                    Error = false,
                    ErrorMsg = ""
                };
            }
            return new TWorkService.Models.LoginReturnModel
            {
                UserId = "",
                UserLogin = "",
                Password = "",
                Error = true,
                ErrorMsg = "User login or password is incorrect"
            };
        }
    }
}
