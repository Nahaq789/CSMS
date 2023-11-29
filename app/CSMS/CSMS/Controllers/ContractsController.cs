using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController
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
        public async Task<ContractModel> GetByID(Guid id)
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
        public async Task<Guid> Add([FromBody] ContractModel contract)
        {
            await _contractService.Add(contract);

            return contract.ContractId;
        }
    }
}
