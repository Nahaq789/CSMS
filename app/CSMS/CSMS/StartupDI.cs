using CSMS.Domain.DomainService;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Infrastracture.Repository.Task;
using CSMS.UseCase.Behavior;
using MediatR;
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

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        builder.Services.AddScoped<ICustomerService<CustomerModel>, CustomerService>();
        builder.Services.AddScoped<IContractService<ContractModel>, ContractService>();
        builder.Services.AddScoped<ITaskService<TaskModel>, TaskService>();

        builder.Services.AddScoped<ITaskRepository<TaskModel>, TaskRepository>();
    }
}