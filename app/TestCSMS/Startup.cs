using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace TestCSMS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var dbFixture = new TestDatabaseFixture();

            services.AddTransient<ApplicationDbContext>( provider => dbFixture.CreateContext());
        }

        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
              .ConfigureWebHost(builder => builder
                            .UseTestServer(options => options.PreserveExecutionContext = true)
                            .UseStartup<AspNetCoreStartup>())
              .ConfigureHostConfiguration(builder => { })
              .ConfigureAppConfiguration((context, builder) => { });
        }

        private class AspNetCoreStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                // services.AddLogging(lb => lb.AddXunitOutput());
            }

            public void Configure(IApplicationBuilder app)
            {
                app.Run(static context => context.Response.WriteAsync(Guid.NewGuid().ToString()));
            }
        }
    }
    
}
