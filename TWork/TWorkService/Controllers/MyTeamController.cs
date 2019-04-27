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
    public class MyTeamController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ITeamService _teamService;
        private IUserService _userService;

        public MyTeamController(IUserRepository userRepository, ITeamService teamService, IUserService userService)
        {
            _userRepository = userRepository;
            _teamService = teamService;
            _userService = userService;
        }

        [HttpPost("GetTeams")]
        public async Task<List<MyTeamsForNavigationViewModel>> GetTeams([FromBody()] TWorkService.Models.LoginModel loginModel)
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
                List<MyTeamsForNavigationViewModel> model = _teamService.GetUserTeamsForNavigation(user).ToList();
                return model;
            }
            else
                return null;
        }
    }
}