using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CSMS.Test.Customers
{
    public class TestCustomerService
    {
        private IConfiguration _configuration;
        public async Task<CustomerModel> GetByID(Guid id)
        {
            var context = _configuration.GetConnectionString("DefaultConnection");
            var option = new ApplicationDbContext(context);
        }
    }
}
