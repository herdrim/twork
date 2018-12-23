using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.Repositories;
using TWork.Models.ViewModels;

namespace TWork.Models.ModelValidators.Concrete
{
    public class RegisterUserModelValidator : IRegisterUserModelValidator
    {
        private IUserRepository _userRepository;

        public RegisterUserModelValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ValidateRegisterUserModel(RegisterUserModel userModel, ModelStateDictionary ModelState)
        {
            bool isError = false;

            USER email = await _userRepository.GetUserByEmail(userModel.Email);
            if (email != null)
            {
                ModelState.AddModelError(nameof(RegisterUserModel.Email), "E-mail is already taken");
                isError = true;
            }
            USER userName = await _userRepository.GetUserByUserName(userModel.UserName);
            if (userName != null)
            {
                ModelState.AddModelError(nameof(RegisterUserModel.UserName), "UserName is already taken");
                isError = true;
            }
            if (userModel.Password.Length < 4)
            {
                ModelState.AddModelError(nameof(RegisterUserModel.Password), "Password is too short");
                isError = true;
            }

            return isError;
        }
    }
}
