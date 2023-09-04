using CSMS.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<CustomerModel> Customers { get; set; }

    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    //{
        
    //}
    //public DbSet<CSMS.Models.CustomerModel> Entities { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Host=db;Port=5432;Database=CustomerMS;Username=postgre;Password=postgre");
    //    }
    //}
    private readonly IConfiguration configuration;
    public ApplicationDbContext (IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // テーブルを再構築しないようにする
        modelBuilder.Entity<CustomerModel>().ToTable("Customers");
    }

}