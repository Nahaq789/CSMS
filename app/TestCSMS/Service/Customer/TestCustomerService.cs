using CSMS.Controllers;
using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using NuGet.ContentModel;
using TestCSMS;
using static CSMS.GlobalEnum.GlobalEnum;

namespace TestCSMS.Service.Customer
{
    public class TestCustomerService : IClassFixture<TestDatabaseFixture>
    {
        private ApplicationDbContext _context { get; set; }
        public TestCustomerService(ApplicationDbContext context)
        {
            _context = context;
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
            Assert.IsAssignableFrom<ICustomerService<CustomerModel>>(reader);
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

            await reader.Update(AfterCustomer);
            var select = await reader.GetByID(beforeCustomer.CustomerId);
            Assert.NotEqual(beforeCustomer.Name, select.Name);
            Assert.NotEqual(beforeCustomer.Email, select.Email);
            Assert.NotEqual(beforeCustomer.Age, select.Age);
            Assert.Equal(beforeCustomer.CustomerId, select.CustomerId);
        }

        [Fact]
        public async void Delete()
        {
            var reader = new CustomerService(_context);

            CustomerModel deleteCustomer = new CustomerModel(
                    "delete",
                    "delete@email.com",
                    300
                );

            await reader.Add(deleteCustomer);
            var result = await reader.Delete(deleteCustomer);
            Assert.Equal(DeleteResult.Success, result);
        }
        [Fact]
        public async void GetAllController()
        {
            var controller = new CustomerController(new CustomerService(_context));

            var result = await controller.GetAll();

            Assert.NotNull(result);
        }
    }
}
