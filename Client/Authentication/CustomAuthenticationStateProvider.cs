using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using System.Security.Claims;
using Blazored.SessionStorage;

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
                var userSession = await _sessionStorage.GetItemAsync<Supabase.Gotrue.Session>("user_session");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsPrincipal = new ClaimsPrincipal(
                    new ClaimsIdentity(new List<Claim>
                        {
                                new Claim(ClaimTypes.Email, userSession.User.Email),
                                new Claim(ClaimTypes.Role, userSession.User.Role)
                        }, "JwtAuth")
                    );

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(Supabase.Gotrue.Session? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, userSession.User.Email),
                        new Claim(ClaimTypes.Role, userSession.User.Role)
                    }));
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
                var userSession = await _sessionStorage.GetItemAsync<Supabase.Gotrue.Session>("user_session");
                if (userSession != null && !userSession.Expired())
                    result = userSession.AccessToken;
            }
            catch { }

            return result;
        }
    }
}
