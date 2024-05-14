using Microsoft.Extensions.DependencyInjection;
using Supabase;
using Supabase.Interfaces;


namespace StellarJadeManager.Server.Extensions
{
    public static class SupabaseClientExtensions
    {
        public static IServiceCollection AddSupabaseClient(this IServiceCollection services, IConfiguration configuration)
        {
            var supabaseUrl = configuration["Supabase:URL"];
            var supabaseKey = configuration["Supabase:PublicApiKey"];
            services.AddSingleton<Supabase.Client>(new Supabase.Client(supabaseUrl, supabaseKey));
            return services;
        }
    }
}
