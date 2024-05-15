using Microsoft.AspNetCore.Mvc;
using StellarJadeManager.Shared;
using Supabase.Interfaces;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Security.Principal;
using StellarJadeManager.Server.Services;

namespace StellarJadeManager.Server.Controllers
{
    [Route("/api/user")]
    public class UserController : ControllerBase
    {

        private readonly Supabase.Client supabaseClient;

        private PostgresContext _db;

        private IUserService _userService;

        public UserController(Supabase.Client supabaseClient, PostgresContext db, IUserService userService)
        {
            this.supabaseClient = supabaseClient;
            _db = db;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO credentials)
        {
            try
            {
                var result = await _userService.SignUp(credentials.Name, credentials.Email, credentials.Password);
                string json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            try
            {
                var result = await _userService.SignIn(credentials.Email, credentials.Password);
                string json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
