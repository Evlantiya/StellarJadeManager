using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StellarJadeManager.Server.Extensions
{
    public static class JWTAuthorizationExtension
    {
        public static IServiceCollection AddJWTAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["Supabase:JWTKey"];
            var validIssuer = "StellarJadeManager";
            var signatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //var validIssuers = $"https://{supabaseProjectId}.supabase.co/auth/v1";
            var validAudience = "authenticated";

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,

                    ValidateAudience = true,
                    ValidAudience = validAudience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signatureKey,

                    //ValidateLifetime= true
                };
            });

         
            services.AddAuthorization();
            return services;
        }
    }
}
