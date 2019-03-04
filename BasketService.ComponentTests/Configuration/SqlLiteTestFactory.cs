using System;
using System.Collections.Generic;
using BasketService.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.ComponentTests.Configuration
{
    public class SqlLiteTestFactory : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseEnvironment(EnvironmentName.Development)
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {

                    services.AddScoped(provider =>
                    {
                        var builder = new DbContextOptionsBuilder<BasketDbContext>(
                            new DbContextOptions<BasketDbContext>(
                                new Dictionary<Type, IDbContextOptionsExtension>()));

                        builder.UseApplicationServiceProvider(provider);

                        builder.UseSqlite(TestConstants.ConnectionString);

                        return builder.Options;
                    });
                });
        }
    }
}
