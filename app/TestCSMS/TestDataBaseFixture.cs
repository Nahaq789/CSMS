using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Transactions;
using TestCSMS.DB;

namespace TestCSMS
{
    public class TestDatabaseFixture
    {
        private string ConnectionString = "Server=localhost;" + "Port=5432;" + "Database=TestCustomerMS;" + "Username=postgres;" + "Password=postgres;";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        /// <summary></summary>
        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        try
                        {
                            context.Database.EnsureDeleted();
                            context.Database.EnsureCreated();

                            GlobalSeed.SetSeeds(context);

                            context.SaveChanges();
                        }catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                        
                    }

                    _databaseInitialized = true;
                }
            }
        }

        /// <summary>
        /// DbContextの生成
        /// </summary>
        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseNpgsql(ConnectionString)
                    .EnableSensitiveDataLogging()
                    .LogTo(message => System.Diagnostics.Debug.WriteLine(message))
                    .Options);
    }
}
