using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using CSMS.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
StartupDI.Setup(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

        services.AddDbContext<ApplicationDbContext>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}

public class AppGlobalObject
{
    public static IConfiguration Configuration { get; set; }
}