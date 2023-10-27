using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerService<CustomerModel> _customerService;

        public CustomerController(ICustomerService<CustomerModel> customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet(Name = "AllCustomers")]
        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            var result = await _customerService.GetAll();
            return result;
        }

        public async Task<CustomerModel> GetByID(Guid id) 
        {
            var result = await _customerService.GetByID(id);
            return result;
        }
    }
}
