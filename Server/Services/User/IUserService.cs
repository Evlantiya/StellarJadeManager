using Microsoft.AspNetCore.Mvc;
using StellarJadeManager.Shared;

namespace StellarJadeManager.Server.Services
{
    public interface IUserService
    {
        Task<UserSession> SignIn(string email, string password);
        Task<UserSession> SignUp(string username, string email, string password);

        Task Logout();
    }
}
