using Microsoft.Extensions.DependencyInjection;
using SafetyFund.Data.Repositories;

namespace SafetyFund.Data
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CampaignRepo>();
            services.AddSingleton<ProjectRepo>();
            services.AddSingleton<UserRepo>();
            services.AddSingleton<VoteRepo>();
            services.AddSingleton<LocationRepo>();
        }
    }
}
