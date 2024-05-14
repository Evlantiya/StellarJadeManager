using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StellarJadeManager.Server.Extensions
{
    public static class SupabaseAuthExtensions
    {
        public static IServiceCollection AddSupabaseAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var supabaseSecretKey = configuration["Supabase:JWTKey"];
            var validIssuer = configuration["Supabase:URL"] + "/auth/v1";
            var supabaseSignatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseSecretKey));
            //var validIssuers = $"https://{supabaseProjectId}.supabase.co/auth/v1";
            var validAudience = "authenticated";

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = validIssuer,

                    ValidateAudience= true,
                    ValidAudience = validAudience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = supabaseSignatureKey,

                    //ValidateLifetime= true
                };
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = "https://your-project.supabase.co/auth/v1";
            //        options.Audience = "authenticated";
            //        options.RequireHttpsMetadata = false; // только для разработки!
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {

            //            // указывает, будет ли валидироваться издатель при валидации токена
            //            ValidateIssuer = true,
            //            // будет ли валидироваться потребитель токена
            //            ValidateAudience = true,
            //            // будет ли валидироваться время существования
            //            ValidateLifetime = true,
            //            // валидация ключа безопасности
            //            ValidateIssuerSigningKey = true,
            //        };
            //    });
            services.AddAuthorization();
            return services;
        }
    }
}
