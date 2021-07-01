using BaseApp.Api.Extensions;
using BaseApp.Application;
using BaseApp.Infrastructure.Data.Context;
using BaseApp.Infrastructure.Identity.Authentication;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace BaseApp
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
            services.AddControllers();
            services.AddAutoMapper(AssemblyInfo.Assembly);
            services.AddMediatR(AssemblyInfo.Assembly);

            services.AddDbContext<BaseAppDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString(nameof(BaseAppDbContext)));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BaseApp", Version = "v1" });
            });

            var tokenConfig = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration
                    .GetSection(nameof(TokenConfiguration)))
                .Configure(tokenConfig);

            services.AddMemoryCache();
            services.AddJwtSecurity(tokenConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger,
            IdentityDbInitializer identityInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BaseApp v1"));

            app.ConfigureExceptionHandler(logger);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            identityInitializer.Initialize().Wait();
        }
    }
}