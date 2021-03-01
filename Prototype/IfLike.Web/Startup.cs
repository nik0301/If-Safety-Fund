using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IFLike.DAL.Context;
using IFLike.DAL.Seeds;
using IFLike.Services;
using IFLike.Domain;
using IfLike.Web.SignalR;

namespace IfLike.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IFLikeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IFLikeContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId ="1521598354575145";
                facebookOptions.AppSecret = "d5cec77e269fa5de45781bdbbf380e4a";
            });

            DependencyInjections.AddDependencyInjections(services);
            services.AddMvc();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IFLikeContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "images",
                    template: "images/{imageID}/{*filename}",
                    defaults: new { controller = "PollImage",action = "Index" });

        });

            List<IIFLikeSeeder> seeders = new List<IIFLikeSeeder> { new CountrySeeder() };
            seeders.ForEach(s => s.Seed(context));

            app.UseSignalR(routes =>
            {
                routes.MapHub<VoteHub>("votehub");
            });
        }
    }
}
