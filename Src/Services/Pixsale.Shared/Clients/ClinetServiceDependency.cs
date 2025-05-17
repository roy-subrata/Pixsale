using Microsoft.Extensions.DependencyInjection;
using Pixsale.Shared.Services;

namespace Pixsale.Shared.Clients
{
    public static class ClinetServiceDependency
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {

            var device = services.BuildServiceProvider()
                .GetRequiredService<IDeviceInfoService>();

            Action<HttpClient> httpClient = ((c) =>
            {
                c.BaseAddress = new Uri(device.GetApiConfiguration().BaseUrl);
            });
            services.AddHttpClient<BranchClient>(httpClient);

            services.AddHttpClient<CustomerClient>(httpClient);

            services.AddHttpClient<UnitClient>(httpClient);

            services.AddHttpClient("CategoryApi", httpClient);

            services.AddHttpClient("UnitApi", httpClient);

            services.AddHttpClient("ProductApi", httpClient);

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
