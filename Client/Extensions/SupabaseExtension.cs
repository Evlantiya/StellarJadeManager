using Microsoft.Extensions.DependencyInjection;
using Supabase;
using Supabase.Interfaces;

namespace StellarJadeManager.Client.Extensions
{
    public static class SupabaseServiceExtensions
    {
        public static IServiceCollection AddSupabase(this IServiceCollection services, string supabaseUrl, string supabaseKey)
        {
            services.AddSingleton<Supabase.Client>(new Supabase.Client(supabaseUrl, supabaseKey));
            return services;
        }
    }
}
