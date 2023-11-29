using CSMS.DomainService;
using CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSMS.Models.ValueObject;

namespace TestCSMS.Service.Contract
{
    public class TestContractService : IClassFixture<TestContractService>
    {
        private ApplicationDbContext _context;

        public TestContractService(ApplicationDbContext context)
        {
            this._context = context;
        }

        //[Fact]
        //public async void GetByID()
        //{
        //    var reader = new ContractService(_context);

        //    ContractModel contract = new ContractModel(
        //            Guid.NewGuid(),
        //            "naha house",
        //            "000789",
        //            Guid.Empty,
        //            new AmountExcludingTax(10000)
        //        );
        //    await reader.Add( contract );
        //    var result = await reader.GetByID(contract.ContractId);

        //    Assert.Equal(contract.ContractId, result.ContractId);
        //    Assert.NotNull(result);
        //}
    }
}
