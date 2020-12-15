using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Standard;
using Application.Notifications;
using Application.Notifications.Domain;
using Application.Services.Domain;
using Application.Services.Standard;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Application.IoC.Services
{
    public static class ServicesIoC
    {
        public static void ApplicationServicesIoC(this IServiceCollection services)
        {
            addDomainServices(services);
            addDomainNotifications(services);
        }

        private static void addDomainServices(IServiceCollection services)
        {
            // Add IoC Generico e dinamico.
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
            services.AddScoped<IPedidoItemService, PedidoItemService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IStatusPedidoService, StatusPedidoService>();
        }

        private static void addDomainNotifications(IServiceCollection services)
        {
            // Add IoC Generico e dinamico.
            services.AddScoped<PedidoNotificationMessages>();
            services.AddScoped<NotificationContext>();
            services.AddScoped<PedidoItemNotificationMessages>();
        }
    }
}