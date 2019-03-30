using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private TWorkDbContext _ctx;
        private UserManager<USER> _userManager;
        private SignInManager<USER> _signInManager;

        public UserRepository(TWorkDbContext context, UserManager<USER> userManager, SignInManager<USER> signInManager)
        {
            _ctx = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IEnumerable<USER> GetAllUsers() => _userManager.Users;

        public async Task<USER> GetUserByEmail(string email) 
            => await _userManager.FindByEmailAsync(email);

        public async Task<USER> GetUserByUserName(string userName) 
            => await _userManager.FindByNameAsync(userName);

        public async Task<USER> GetUserByContext(ClaimsPrincipal user)
            => await _userManager.GetUserAsync(user);

        public async Task<USER> GetUserById(string userId)
            => await _userManager.FindByIdAsync(userId);

        public IEnumerable<USER> GetUsersByTeam(TEAM team)
            => _ctx.USERS_TEAMs.Where(x => x.TEAM == team).Select(x => x.USER);
    }
}
