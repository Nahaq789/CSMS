using CSMS.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<CustomerModel> Customers { get; set; }
    private readonly IConfiguration _configuration;

    public ApplicationDbContext() { }
    public ApplicationDbContext (DbContextOptions <ApplicationDbContext> options) : base(options)
    {
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
    //}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // テーブルを再構築しないようにする
        modelBuilder.Entity<CustomerModel>().ToTable("Customers");
    }
} 