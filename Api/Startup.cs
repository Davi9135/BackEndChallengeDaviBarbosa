using Api.Filters;
using Application.IoC.Services;
using Infrastructure.IoC;
using Infrastructure.IoC.ORMs.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // IoC Camada de serviçoes.
            services.ApplicationServicesIoC();
            // IoC Da camada de banco de dados. Vamos usar o EF Core.            
            services.InfrastructureORM<EntityFrameworkIoC>();
            addNewtonsoftJson(services);            
            addDomainNotificationFilter(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void addNewtonsoftJson(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        private void addDomainNotificationFilter(IServiceCollection services)
        {            
            services.AddMvc(options => options.Filters.Add<NotificationFilter>());
        }
    }
}