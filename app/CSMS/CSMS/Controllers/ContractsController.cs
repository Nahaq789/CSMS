using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractService<ContractModel> _contractService;

        public ContractsController(IContractService<ContractModel> contractService)
        {
            _contractService = contractService;
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
        public async Task<IActionResult> PostAsync([FromBody] ContractModel contract)
        {
            await _contractService.Add(contract);
            return Ok(contract);
        }

        [HttpPut]
        public async Task<GlobalEnum.GlobalEnum.UpdateResult> Update(ContractModel contract)
        {
            try
            {
                await _contractService.Update(contract);
                return GlobalEnum.GlobalEnum.UpdateResult.Success;
            }
            catch (Exception ex)
            {
                return GlobalEnum.GlobalEnum.UpdateResult.Failed;
            }
        }
        [HttpDelete]
        public async Task<GlobalEnum.GlobalEnum.DeleteResult> Delete(ContractModel contract)
        {
            try
            {
                await _contractService.Delete(contract);
                return GlobalEnum.GlobalEnum.DeleteResult.Success;
            }
            catch(Exception ex)
            {
                return GlobalEnum.GlobalEnum.DeleteResult.Failed;
            }
        }
    }
}
