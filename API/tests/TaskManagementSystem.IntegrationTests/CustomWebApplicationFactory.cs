using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveConnectionToMongoDB(services);

            services.AddSingleton<ITaskRepository, LocalRepository>();
        });
    }

    private void RemoveConnectionToMongoDB(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(ITaskRepository));

        if (descriptor is not null)
            services.Remove(descriptor);
    }
}