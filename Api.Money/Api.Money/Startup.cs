using System.Net;
using Api.Money.ExceptionHandler;
using Api.Money.Helpers;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Settings;
using Services;
using Services.Interfaces;

namespace Api.Money
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
            services.AddDbContext<WalletContext>(options =>
                options.UseSqlServer(Configuration.GetSection("SqlConnection:ConnectionString").Value));
            
            services.AddControllers();

            services.Configure<ConnectionToDb>(options =>
            {
                options.ConnectionString = Configuration.GetSection("SqlConnection:ConnectionString").Value;
            });

            services.Configure<ApiRate>(options =>
            {
                options.Url = Configuration.GetSection("ApiRate:Url").Value;
            });

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IMoneyService, MoneyService>();
            services.AddTransient<IRateService, ApiRateClient>();           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
