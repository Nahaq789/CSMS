using CSMS.Models;
using CSMS.Models.ValueObject;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<ContractModel> Contracts { get; set; }

    public ApplicationDbContext() { }
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // テーブルを再構築しないようにする
        modelBuilder.Entity<CustomerModel>().ToTable("Customers");
        modelBuilder.Entity<ContractModel>().ToTable("Contracts");

        modelBuilder.Entity<ContractModel>(entity => entity.OwnsOne(m => m.Money).Property(m => m.Money).HasColumnName("Money"));
        modelBuilder.Entity<ContractModel>(entity => entity.OwnsOne(m => m.Money).Property(m => m.TaxMoney).HasColumnName("TaxMoney"));
    }
} 