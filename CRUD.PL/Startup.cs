using AspNetCoreRateLimit;
using CRUD.BLL.CQRS;
using CRUD.BLL.Interfaces;
using CRUD.BLL.Repositories;
using CRUD.DAL.Contexts;
using CRUD.DAL.Models.Identity;
using CRUD.PL.Helpers;
using CRUD.PL.Mapping;
using CRUD.PL.Settings;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
namespace CRUD.PL
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
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(Options => 
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddIdentity<User, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;//@ # $
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequiredLength = 8;
            })
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options =>
            {
                Options.LoginPath = "/Account/Login";
                Options.AccessDeniedPath = "/Home/Error";
                Options.ExpireTimeSpan = TimeSpan.FromDays(1);
                Options.LogoutPath = "/Account/Login";
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<ImailSettings, EmailSettings>();

            services.AddMediatR(typeof(CQRSLip).Assembly);

            // Add needed services for Rate Limiting
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            // Configure Hangfire to use SQL Server storage
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                      {
                          CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                          SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                          QueuePollInterval = TimeSpan.Zero,
                          UseRecommendedIsolationLevel = true,
                          UsePageLocksOnDequeue = true,
                          DisableGlobalLocks = true
                      }));

            // Add the Hangfire server
            services.AddHangfireServer();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Add Rate Limiting Middleware
            app.UseIpRateLimiting();

            // Enable the Hangfire Dashboard
            app.UseHangfireDashboard("/hangfire");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
