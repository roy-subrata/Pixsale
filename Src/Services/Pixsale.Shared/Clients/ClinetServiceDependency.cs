using Microsoft.Extensions.DependencyInjection;

namespace Pixsale.Shared.Clients
{
    public static class ClinetServiceDependency
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services)
        {
            services.AddHttpClient<BranchClient>(client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5262/");
            });

            services.AddHttpClient<CustomerClient>(client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5262/");
            });
            return services;
        }
    }
}
