using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.Entities;
using TWork.Models.ViewModels;

namespace TWork.Models.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(LoginUserViewModel userModel);
        Task<IdentityResult> CreateUserAsync(RegisterUserViewModel registerUserModel);
        Task SingOutAsync();
        Task<bool> AssignUserToTeamWithBasicRole(string userId, string assignerId, int teamId);
    }
}
