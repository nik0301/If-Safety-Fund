using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SafetyFund.Data;
using SafetyFund.Web.Controllers.Authentication;

namespace SafetyFund.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string ErrorPage = "/Home/Error";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddAzureAd(options => Configuration.Bind("AzureAd", options))
                .AddCookie();

            services.AddDbContext<SafetyFundDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                services.AddSingleton(options.Options);
            });


            var sp = services.BuildServiceProvider();
            sp.GetService<SafetyFundDbContext>();

            Business.Startup.ConfigureServices(services);

            EnableCors(services);

            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1000);
                options.Cookie.HttpOnly = true;
            });
        }

        /// <summary>
        /// Enable Cross site Ajax requists. On production enable only requests from www.if.lv site.
        /// Example taken from: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.0
        /// </summary>
        /// <param name="services"></param>
        private void EnableCors(IServiceCollection services)
        {
            var corsSites = Configuration.GetSection("Cors")
                .GetValue<string>("AllowSpecificOrigins")
                .Split(',');

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins(corsSites)
                        .WithMethods("GET", "POST", "HEAD"));
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ErrorPage);
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
