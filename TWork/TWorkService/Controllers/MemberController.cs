using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.Services;
using TWork.Models.ViewModels;
using TWorkService.Models;

namespace TWorkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        IUserRepository _userRepository;
        ITeamRepository _teamRepository;
        IUserService _userService;
        ITeamService _teamService;

        public MemberController(IUserRepository userRepository, ITeamRepository teamRepository, IUserService userService, ITeamService teamService)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _userService = userService;
            _teamService = teamService;
        }

        [HttpPost("GetMembers")]
        public async Task<List<MemberViewModel>> GetMembers([FromBody] UserTeamModel teamModel)
        {
            LoginUserViewModel details = new LoginUserViewModel
            {
                Email = teamModel.UserLogin,
                Password = teamModel.Password
            };

            bool succeeded = await _userService.LoginAsync(details);
            if (succeeded)
            {
                USER user = await _userRepository.GetUserByEmail(details.Email);
                if (_teamRepository.IsTeamMember(user, teamModel.TeamId))
                {
                    TeamMemberViewModel model = _teamService.GetTeamMembers(teamModel.TeamId);
                    return model.Members;                    
                }
            }
            return null;
        }
    }
}