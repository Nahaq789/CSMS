using AutoMapper;
using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.DTO.Contract;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService<ContractModel> _contractService;
        private readonly IMapper _mapper;
        public ContractsController(IContractService<ContractModel> contractService, IMapper mapper)
        {
            this._contractService = contractService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractModel>> GetAll()
        {
            try
            {
                var result = await _contractService.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        [HttpGet("{id}")]
        public async Task<ContractModel> GetById(Guid id)
        {
            try
            {
                var result = await _contractService.GetByID(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] ContractDto contract)
        {
            try
            {
                var _contract = _mapper.Map<ContractModel>(contract);
                if (ModelState.IsValid)
                {
                    await _contractService.Add(_contract);
                    return Ok(contract);
                }
                else
                {
                    return BadRequest("Failed to create contract");
                }
            }
            catch (Exception ex)
            {
                var result =
                    $"It was not possible to create a new contract, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromBody] ContractModel contract)
        {
            try
            {
                var result = await _contractService.Update(contract);
                return result == GlobalEnum.GlobalEnum.UpdateResult.Success
                    ? Ok(contract)
                    : BadRequest("Failed to update contract");

            }
            catch (Exception ex)
            {
                var result =
                    $"It was not possible to update a new contract, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(ContractModel contract)
        {
            try
            {
                var result = await _contractService.Delete(contract);
                return result == GlobalEnum.GlobalEnum.DeleteResult.Success
                    ? Ok(contract)
                    : BadRequest("Failed to delete contract");
            }
            catch(Exception ex)
            {
                var result =
                    $"It was not possible to delete a new order, please try later on ({ex.GetType().Name} - {ex.Message})";
                return BadRequest(result);
            }
        }
    }
}
