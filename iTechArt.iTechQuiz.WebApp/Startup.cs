using System;
using System.Collections.Generic;
using iTechArt.iTechQuiz.Foundation.Services;
using iTechArt.iTechQuiz.Repositories.Context;
using iTechArt.iTechQuiz.Repositories.UnitOfWork;
using iTechArt.iTechQuiz.WebApp.Providers;
using iTechArt.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace iTechArt.iTechQuiz.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<iTechQuizContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var mvcBuilder = services.AddControllersWithViews();
            if (Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Tokens.PasswordResetTokenProvider = PasswordResetTokenProviderOptions.ProviderName;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<iTechQuizContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<PasswordResetTokenProvider<IdentityUser<Guid>>>(PasswordResetTokenProviderOptions.ProviderName);

            services.Configure<PasswordResetTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromMinutes(10));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            services.AddTransient<IAppVersionService, AppVersionService>();

            services.Configure<EmailServiceOptions>(Configuration.GetSection("EmailServiceOptions"));
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    .RequireAuthorization();
            });
        }
    }
}
