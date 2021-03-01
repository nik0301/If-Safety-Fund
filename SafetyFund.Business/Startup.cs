using Microsoft.Extensions.DependencyInjection;

namespace SafetyFund.Business
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Data.Startup.ConfigureServices(services);

            services.AddSingleton<ProjectList>();
            services.AddSingleton<CampaignList>();
            services.AddSingleton<Autorization>();
            services.AddSingleton<UserList>();
            services.AddSingleton<VoteList>();
            services.AddSingleton<LocationList>();
            services.AddSingleton<LocationService>();
        }
    }
}
