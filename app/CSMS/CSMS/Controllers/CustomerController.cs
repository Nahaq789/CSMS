using AutoMapper;
using CSMS.DomainInterface;
using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.DTO;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService<CustomerModel> _customerService;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerService<CustomerModel> customerService, IMapper mapper)
        {
            this._customerService = customerService;
            this._mapper = mapper;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CustomerDto customer)
        {
            try
            {
                var _customer = _mapper.Map<CustomerModel>(customer);
                if (ModelState.IsValid)
                {
                    await _customerService.Add(_customer);
                    return Ok(customer);
                }
                else
                {
                    return BadRequest("Failed to create customer");
                }
            }
            catch (Exception ex)
            {
                var result =
                    $"It was not possible to create a new customer, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CustomerModel customer)
        {
            try
            {
                var result = await _customerService.Update(customer);
                return result == GlobalEnum.GlobalEnum.UpdateResult.Success
                    ? Ok(customer)
                    : BadRequest("Failed to update customer");
            }
            catch (Exception ex)
            {
                var result =
                    $"It was not possible to update a new customer, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] CustomerModel customer)
        {
            try
            {
                var result = await _customerService.Delete(customer);
                return result == GlobalEnum.GlobalEnum.DeleteResult.Success
                    ? Ok(customer)
                    : BadRequest("Failed to delete customer");
            }
            catch (Exception ex)
            {
                var result =
                    $"It was not possible to delete a new order, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }
    }
}
