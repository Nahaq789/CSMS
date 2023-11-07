using CSMS.DomainService;


namespace CSMS.Test.Customers
{
    public class Startup
    {
        public class TestCustomerService : IDisposable
        {
            private ApplicationDbContext _context { get; }
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

            public void Dispose()
            {

            }

            public void Test_NotIMplemented()
            {
                throw new NotImplementedException();
            }
        }
    }
    
}
