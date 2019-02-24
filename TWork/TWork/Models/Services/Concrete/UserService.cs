using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class UserService : IUserService
    {
        UserManager<USER> _userManager;
        SignInManager<USER> _signInManager;
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        ITeamRepository _teamRepository;
        IMessageService _messageService;

        public UserService(UserManager<USER> usrManager, SignInManager<USER> signManager, IUserRepository userRepository, IRoleRepository roleRepository, ITeamRepository teamRepository, IMessageService messageService)
        {
            _userManager = usrManager;
            _signInManager = signManager;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
            _messageService = messageService;
        }


        public async Task<bool> LoginAsync(LoginUserViewModel userModel)
        {
            bool retVal = false;
            USER user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();                
                SignInResult result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
                retVal = result.Succeeded;
            }

            return retVal;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUserViewModel registerUserModel)
        {
            USER user = new USER
            {
                Email = registerUserModel.Email,
                UserName = registerUserModel.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerUserModel.Password);

            return result;
        }

        public async Task SingOutAsync() 
            => await _signInManager.SignOutAsync();

        public async Task<bool> AssignUserToTeamWithBasicRole(string userId, string assignerId, int teamId)
        {
            TEAM team = _teamRepository.GetTeamById(teamId);
            USER user = await _userRepository.GetUserById(userId);

            if (team != null && user != null)
            {
                USER_TEAM userTeam = new USER_TEAM
                {
                    TEAM = team,
                    USER = user
                };

                ROLE basicRole = _roleRepository.GetBasicRole();
                USER_TEAM_ROLES userTeamRole = new USER_TEAM_ROLES
                {
                    USER = user,
                    TEAM = team,
                    ROLE = basicRole
                };

                _teamRepository.AssignUserToTeamWithRole(userTeam, userTeamRole);
                await _messageService.RemoveTeamJoinRequestByUserFrom(user.Id, team.ID);
                await _messageService.CreateNewMessageForUser(user.Id, assignerId, "You have been assigned to " + team.NAME);
                return true;
            }
            else
                return false;
        }
    }
}
