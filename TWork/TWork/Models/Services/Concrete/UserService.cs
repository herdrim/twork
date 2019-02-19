using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services.Concrete
{
    public class UserService : IUserService
    {
        private UserManager<USER> _userManager;
        private SignInManager<USER> _signInManager;

        public UserService(UserManager<USER> usrManager, SignInManager<USER> signManager)
        {
            _userManager = usrManager;
            _signInManager = signManager;
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
    }
}
