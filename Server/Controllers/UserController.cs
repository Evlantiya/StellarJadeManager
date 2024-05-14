using Microsoft.AspNetCore.Mvc;
using StellarJadeManager.Shared;
using Supabase.Interfaces;
using System.Net.Http;
using Newtonsoft;
using static StellarJadeManager.Client.Components.Registration;
using Newtonsoft.Json;
using System.Security.Principal;

namespace StellarJadeManager.Server.Controllers
{
    [Route("/api/user")]
    public class UserController : ControllerBase
    {

        private readonly Supabase.Client supabaseClient;

        public UserController(Supabase.Client supabaseClient)
        {
            this.supabaseClient = supabaseClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentials credentials)
        {
            try
            {
                var response = await supabaseClient.Auth.SignUp(credentials.Email, credentials.Password);
                string json = JsonConvert.SerializeObject(response, Formatting.Indented);
                return Ok(json);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentials credentials)
        {
            try
            {
                var response = await supabaseClient.Auth.SignIn(credentials.Email, credentials.Password);
                string json = JsonConvert.SerializeObject(response, Formatting.Indented);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
