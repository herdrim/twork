using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWork.Models.Entities;

namespace TWork.Models.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<USER> GetAllUsers();

        Task<USER> GetUserByEmail(string email);

        Task<USER> GetUserByUserName(string userName);

        Task<USER> GetUserByContext(ClaimsPrincipal user);
    }
}
