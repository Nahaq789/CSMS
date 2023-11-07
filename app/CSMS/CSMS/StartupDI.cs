using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class StartupDI
{
    public static void Setup(WebApplicationBuilder builder)
    {
        var configure = builder.Configuration;
        var connectionString = configure.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, option) =>
        {
            option.LogTo((message) => System.Diagnostics.Debug.WriteLine(message));
            option.UseNpgsql(connectionString);
        });

    }
}