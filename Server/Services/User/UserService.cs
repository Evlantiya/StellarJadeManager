using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StellarJadeManager.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StellarJadeManager.Server.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _config;
        private PostgresContext _db;

        public UserService(PostgresContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<UserSession> SignIn(string email, string password)
        {
            var user = await _db.Users.FirstAsync(user => user.Email == email);
            var hashed = PasswordHasher.HashPassword(password, Convert.FromBase64String(user.Salt));
            if (user.Hash != Convert.ToBase64String(hashed))
            {
                throw new Exception("Wrong email or password");
            }
            user.LastActive = DateTime.Now;
            var session = GenerateUserSession(user);
            await _db.SaveChangesAsync();
            return session;

        }

        public async Task<UserSession> SignUp(string username, string email, string password)
        {
            var salt = PasswordHasher.GenerateSalt();
            var hash = PasswordHasher.HashPassword(password, salt);
            var user = new User(username, email, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
            //{
            //    Name = username,
            //    Email = email,
            //    Salt = Convert.ToBase64String(salt),
            //    Hash = Convert.ToBase64String(hash),
            //    LastActive = DateTime.Now,
            //};
            var profile = new Server.Profile { User = user };
            user.Profiles.Add(profile);
            await _db.Users.AddAsync(user);
            var session = GenerateUserSession(user);
            await _db.SaveChangesAsync();
            return session;

        }


        private UserSession GenerateUserSession(User user)
        {
            /* Generating JWT Token */
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(120);
            var tokenKey = Encoding.UTF8.GetBytes(_config["Supabase:JWTKey"]);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),

                });
            claimsIdentity.AddClaim(new Claim(type: "LastActive", value: user.LastActive.ToString()));
            claimsIdentity.AddClaim(new Claim(type: "Id", value: user.Id.ToString()));
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "StellarJadeManager",
                
                Audience = "authenticated",
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            var userSession = new UserSession
            {
                Name = user.Name,
                Email = user.Email,
                UserId = user.Id,
                LastActive = user.LastActive,
                Token = token,
                Expiration = tokenExpiryTimeStamp
            };
            return userSession;
        }


    }
}
