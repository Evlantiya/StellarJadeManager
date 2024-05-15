using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using System.Security.Claims;
using Blazored.SessionStorage;
using StellarJadeManager.Shared;
using Supabase.Gotrue;

namespace StellarJadeManager.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.GetItemAsync<UserSession>("user_session");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Name),
                    new Claim(ClaimTypes.Email, userSession.Email),

                }, "JwtAuth");
                claimsIdentity.AddClaim(new Claim(type: "LastActive", value: userSession.LastActive.ToString()));
                claimsIdentity.AddClaim(new Claim(type: "Id", value: userSession.UserId.ToString()));
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                //claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                //    {
                //        new Claim(ClaimTypes.Email, userSession.Email),
                //    }));
                //userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await _sessionStorage.SetItemAsync("user_session", userSession);
            }
            else
            {
                claimsPrincipal = _anonymous;
                await _sessionStorage.RemoveItemAsync("user_session");
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                var userSession = await _sessionStorage.GetItemAsync<UserSession>("user_session");
                if (userSession != null && userSession.Expiration > DateTime.Now )
                    result = userSession.Token;
            }
            catch { }

            return result;
        }
    }
}
