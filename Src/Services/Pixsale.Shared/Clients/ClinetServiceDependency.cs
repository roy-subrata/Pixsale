using System;
using Microsoft.Extensions.DependencyInjection;
using Pixsale.Shared.Services;

namespace Pixsale.Shared.Clients
{
    public static class ClinetServiceDependency
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {
            services.AddHttpClient<BranchClient>((serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });

            services.AddHttpClient<CustomerClient>((serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });

            services.AddHttpClient<UnitClient>((serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });

            services.AddHttpClient("CategoryApi", ((serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            }));

            services.AddHttpClient("UnitApi", ((serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            }));

            services.AddHttpClient("ProductApi",(serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });
            services.AddHttpClient("SupplierApi", (serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });

            services.AddHttpClient("PurchaseApi", (serviceProvider, client) =>
            {
                var scope = serviceProvider.CreateScope();
                var deviceInfoService = scope.ServiceProvider.GetRequiredService<IDeviceInfoService>();
                var apiConfig = deviceInfoService.GetApiConfiguration(); // Use async-safe code in production
                client.BaseAddress = new Uri(apiConfig.BaseUrl);
            });
            return services;
        }
    }
}
