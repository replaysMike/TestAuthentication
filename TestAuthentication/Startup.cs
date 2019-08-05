using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAuthentication.Data.Context;
using TestAuthentication.Data.Identity;
using TestAuthentication.Data.Models;

namespace TestAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var containerOptions = new ContainerOptions { EnablePropertyInjection = true };
            Container = new ServiceContainer(containerOptions);
        }

        public IConfiguration Configuration { get; }
        public ServiceContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //.AddControllersAsServices();  // tell asp.net to use LightInject to create controllers

            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AuthenticationConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // User settings
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddDefaultTokenProviders();


            services.AddScoped<UserStore<ApplicationUser, ApplicationRole, AuthenticationDbContext, int>, ApplicationUserStore>();
            services.AddScoped<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddScoped<ApplicationUserStore>();

            services.AddRouting();

            // // use lightinject to instantiate controllers
            // var serviceProvider = Container.CreateServiceProvider(services);
            // //var userStore = serviceProvider.GetRequiredService<ApplicationUserManager>(); // error
            // //var serviceProvider = services.BuildServiceProvider();
            // var userStore = serviceProvider.GetRequiredService<ApplicationSignInManager>();

        }

        public void ConfigureContainer(IServiceContainer serviceContainer)
        {
            serviceContainer.RegisterAssembly(typeof(ApplicationUserManager).Assembly);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
