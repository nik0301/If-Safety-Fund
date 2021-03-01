using IFLike.DAL.Implementation;
using IFLike.DAL.Interfaces;
using IFLike.Services.Implementation;
using IFLike.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IFLike.Services
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(IServiceCollection services)
        {
            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddTransient<IPollService, PollService>();
            services.AddTransient<ICountryValidationService, CountryValidationService>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IIpLocationRepository, IpLocationRepository>();

            services.AddTransient<IPollResultRepository, PollResultRepository>();
            services.AddTransient<IPollResultService, PollResultService>();

            services.AddTransient<IPollItemRepository, PollItemRepository>();
            services.AddTransient<IPollItemService, PollItemService>();

            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IImageService, ImageService>();
        }
    }
}
