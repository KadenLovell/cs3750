using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Server.Persistence;
using Server.Services;

namespace Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services
              .AddAuthentication("Cookies")
              .AddCookie(x => {
                  x.Cookie.Domain = $".localhost:4200";
                  x.Cookie.Name = "3750auth_ticketDev_";
                  x.Events.OnRedirectToLogin += context => {
                      context.Response.Redirect($"https://localhost:4200/login");

                      return Task.CompletedTask;
                  };
                  x.Events.OnRedirectToLogout += context => {
                      context.Response.Redirect($"https://localhost:4200/login");

                      return Task.CompletedTask;
                  };
              });

            services.AddScoped<IPersistenceContext, DataContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddControllers();
            services.AddCors();

            // Added Newtonsoft JSON parser for controllers to be able to parse JSON objects [FromBody] to dynamic objects.
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Add HTTP Context to keep track of login status on the client.
            services.AddHttpContextAccessor();

            // If this were to ever be a production envrionment, we would want to configure this based on the build environment.
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevelopSqlServer")));

            // Example:
            // Services (keep alphabetized, disregarding "Service" suffix)
            services
             .AddScoped<DustinService>()
             .AddScoped<CredentialsService>()
             .AddScoped<LoginService>()
             .AddScoped<RyanService>()
             .AddScoped<UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => options.WithOrigins("http://localhost:4200", "https://localhost:4200", "localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
