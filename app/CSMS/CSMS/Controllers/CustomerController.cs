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
        private readonly ICustomerService<CustomerModel> _customerService;

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
        public async Task<CustomerModel> GetById(Guid id) 
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

        [HttpPut]
        public async Task<GlobalEnum.GlobalEnum.UpdateResult> Update([FromBody] CustomerModel customer)
        {
            try
            {
                await _customerService.Add(customer);

                return GlobalEnum.GlobalEnum.UpdateResult.Success;
            }
            catch (Exception ex)
            {
                return GlobalEnum.GlobalEnum.UpdateResult.Failed;
            }
        }

        [HttpDelete]
        public async Task<GlobalEnum.GlobalEnum.DeleteResult> Delete([FromBody] CustomerModel customer)
        {
            try
            {
                await _customerService.Delete(customer);
                return GlobalEnum.GlobalEnum.DeleteResult.Success;
            }
            catch (Exception ex)
            {
                return GlobalEnum.GlobalEnum.DeleteResult.Failed;
            }
        }
    }
}
