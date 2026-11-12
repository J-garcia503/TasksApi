using Microsoft.Extensions.DependencyInjection;
using TasksApi.Application.Interfaces;
using TasksApi.Application.Services;

namespace TasksApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
