using CSMS.Domain.Models;
using CSMS.Domain.Models.Master;
using CSMS.Domain.Models.ValueObject;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<ContractModel> Contracts { get; set; }
    public DbSet<TaskModel> Task { get; set; }
    public DbSet<TaskStatusModel> TaskStatus { get; set; }

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
        modelBuilder.Entity<TaskModel>().ToTable("Task");

        //Master
        modelBuilder.Entity<TaskStatusModel>().ToTable("TaskStatus");

        modelBuilder.Entity<ContractModel>(entity => entity.OwnsOne(m => m.Money).Property(m => m.Value).HasColumnName("Money"));
        modelBuilder.Entity<ContractModel>(entity => entity.OwnsOne(m => m.TaxRate).Property(m => m.Value).HasColumnName("TaxRate"));

        modelBuilder.Entity<TaskModel>()
            .HasOne(t => t._TaskStatusModel)
            .WithMany(ts => ts._Tasks)
            .HasForeignKey(t => t.TaskStatusId)
            .HasConstraintName("TaskStatus_fkey");
    }
} 