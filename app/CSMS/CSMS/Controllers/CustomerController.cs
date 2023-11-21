using CSMS.DomainInterface;
using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerService<CustomerModel> _customerService;

        public CustomerController(ICustomerService<CustomerModel> customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> GetAll()
        {
            try
            {
                var result = await _customerService.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("{id}")]
        public async Task<CustomerModel> GetByID(Guid id) 
        {
            try
            {
                var result = await _customerService.GetByID(id);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        [HttpPost]
        public async Task<Guid> Add([FromBody] CustomerModel customer)
        {
            await _customerService.Add(customer);

            return customer.CustomerId;
        }
    }
}
