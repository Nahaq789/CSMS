using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
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

        builder.Services.AddScoped<ICustomerService<CustomerModel>, CustomerService>();
        builder.Services.AddScoped<IContractService<ContractModel>, ContractService>();
    }
}