using CSMS.Presentation.Controllers;
using CSMS.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using AutoMapper;
using CSMS.Domain.DomainService;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata;
using CSMS.UseCase.Behavior;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_AllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

//DI
StartupDI.Setup(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        AppGlobalObject.Configuration = configuration;
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddScoped<IDomainService<CustomerModel>, CustomerService>();
        //services.AddScoped<IRepository<CustomerModel>, CustomerRepository>();

        services.AddCors();
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ICustomerService<CustomerModel>, CustomerService>();
        
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}

public class AppGlobalObject
{
    public static IConfiguration Configuration { get; set; }
}