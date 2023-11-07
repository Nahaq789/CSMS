using CSMS.DomainService;
using CSMS.Models;
using NuGet.ContentModel;
using TestCSMS;

namespace CSMS.Test.Customers
{
    public class TestCustomerService : IClassFixture<TestDatabaseFixture>
    {
        private ApplicationDbContext _context { get; set; }
        public TestCustomerService(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Fact]
        public async void GetByID()
        {
            var guid = Guid.NewGuid();

            var reader = new CustomerService(_context);
            var result = await reader.GetByID(guid);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetAll()
        {
            var reaser = new CustomerService(_context);
            var result = await reaser.GetAll();

            Assert.NotNull(result);
        }

        [Fact]
        public async void Add()
        {
            var guid = Guid.NewGuid();

            CustomerModel customer = new CustomerModel(
                    guid,
                    "test",
                    "test@email.com",
                    23
                );

            var reader = new CustomerService(_context);
            var result = await reader.Add("test", "test@email.com", 23);

            Asset.Equals(customer, result);
        }

        public void Dispose()
        {

        }

        public void Test_NotIMplemented()
        {
            throw new NotImplementedException();
        }
    }
}
