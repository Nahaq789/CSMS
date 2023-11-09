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
            var reader = new CustomerService(_context);

            CustomerModel customer = new CustomerModel(
                    "test123",
                    "test123@email.com",
                    26
                );
            await reader.Add(customer);
            var result = await reader.GetByID(customer.CustomerId);

            Assert.Equal(customer.CustomerId, result.CustomerId);
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
            CustomerModel customer = new CustomerModel(
                    "test",
                    "test@email.com",
                    23
                );

            var reader = new CustomerService(_context);
            var result = await reader.Add(customer);

            Assert.Equal(customer.CustomerId, result);
        }

        [Fact]
        public async void Update()
        {
            var reader = new CustomerService(_context);
            CustomerModel beforeCustomer = new CustomerModel(
                    "before",
                    "before@email.com",
                    11
                );

            await reader.Add(beforeCustomer);
            CustomerModel AfterCustomer = new CustomerModel(
                    beforeCustomer.CustomerId,
                    "after",
                    "after@email.com",
                    88
                );
            
            var result = await reader.Update(AfterCustomer);
            Assert.Equal(result.CustomerId, AfterCustomer.CustomerId);
            Assert.Equal(result.Name, AfterCustomer.Name);
            Assert.Equal(result.Email, AfterCustomer.Email);
            Assert.Equal(result.Age, AfterCustomer.Age);
        }
    }
}
