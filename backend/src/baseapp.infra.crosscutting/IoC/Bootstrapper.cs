using BaseApp.Domain.IoC;
using BaseApp.Domain.Notifications;
using BaseApp.Infra.CrossCutting.Notification;
using BaseApp.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BaseApp.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationContext, NotificationContext>();
        }
    }
}