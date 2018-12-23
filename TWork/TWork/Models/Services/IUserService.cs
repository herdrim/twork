using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginUserModel userModel);
        Task<IdentityResult> CreateUserAsync(RegisterUserModel registerUserModel);
        Task SingOutAsync();
    }
}
