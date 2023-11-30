using CSMS.DomainService;
using CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSMS.Models.ValueObject;
using CSMS.DomainService.Interface;
using System.Diagnostics.Contracts;
using Microsoft.Build.ObjectModelRemoting;

namespace TestCSMS.Service.Contract
{
    public class TestContractService : IClassFixture<TestContractService>
    {
        private ApplicationDbContext _context;

        public TestContractService(ApplicationDbContext context)
        {
            this._context = context;
        }

        [Fact]
        public async void GetByID()
        {
            var reader = new ContractService(_context);
            TaxRate taxRate = new TaxRate(10m);
            AmountExcludingTax amountExcludingTax = new AmountExcludingTax(10000m);

            ContractModel contract = new ContractModel(
                    Guid.NewGuid(),
                    "naha house",
                    "000789",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(amountExcludingTax, taxRate),
                    taxRate
                );
            await reader.Add(contract);
            var result = await reader.GetByID(contract.ContractId);

            Assert.Equal(contract.ContractId, result.ContractId);
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetAll()
        {
            var reader = new ContractService(_context);

            TaxRate taxRate = new TaxRate(10m);
            AmountExcludingTax amountExcludingTax = new AmountExcludingTax(300000m);

            ContractModel contract = new ContractModel(
                    Guid.NewGuid(),
                    "naha house2",
                    "111789",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(amountExcludingTax, taxRate),
                    taxRate
                );

            await reader.Add(contract);
            var result = await reader.GetAll();

            Assert.NotNull(result);
        }
        [Fact]
        public async void Add()
        {
            var reader = new ContractService(_context);

            TaxRate taxRate = new TaxRate(10m);
            AmountExcludingTax amountExcludingTax = new AmountExcludingTax(3000m);

            ContractModel contract = new ContractModel(
                    Guid.NewGuid(),
                    "naha house3",
                    "785789",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(amountExcludingTax, taxRate),
                    taxRate
                );

            await reader.Add(contract);
            var result = await reader.GetByID( contract.ContractId );

            Assert.IsAssignableFrom<IContractService<ContractModel>>(reader);
            Assert.NotNull(result);
            Assert.Equal(result.ContractId, contract.ContractId );
            Assert.IsType<AmountExcludingTax>(result.Money);
            Assert.IsType<AmountIncludingTax>(result.TaxMoney);
            Assert.IsType<TaxRate>(result.TaxRate);
        }
        [Fact]
        public async void Update()
        {
            var reader = new ContractService(_context);

            TaxRate taxRate = new TaxRate(10m);
            AmountExcludingTax amountExcludingTax = new AmountExcludingTax(8888800m);

            ContractModel beforeContract = new ContractModel(
                    Guid.NewGuid(),
                    "before",
                    "111111",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(amountExcludingTax, taxRate),
            taxRate
                );
            await reader.Add(beforeContract);
            ContractModel afterContract = new ContractModel(
                    beforeContract.ContractId,
                    "after",
                    "333333",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(new AmountExcludingTax(880m), taxRate),
                    taxRate
                );
            await reader.Update(afterContract);

            Assert.IsAssignableFrom<IContractService<ContractModel>>(reader);
            Assert.NotEqual(beforeContract, afterContract);
            Assert.NotEqual(beforeContract.ContractName, afterContract.ContractName);
            Assert.NotEqual(beforeContract.ContractCode, afterContract.ContractCode);
            Assert.IsType<AmountExcludingTax>(afterContract.Money);
            Assert.IsType<AmountIncludingTax>(afterContract.TaxMoney);
            Assert.IsType<TaxRate>(afterContract.TaxRate);
            Assert.Equal(beforeContract.ContractId, afterContract.ContractId);
        }
        [Fact]
        public async void Delete()
        {
            var reader = new ContractService(_context);

            TaxRate taxRate = new TaxRate(10m);
            AmountExcludingTax amountExcludingTax = new AmountExcludingTax(3000m);

            ContractModel contract = new ContractModel(
                    Guid.NewGuid(),
                    "delete",
                    "788789",
                    Guid.Empty,
                    amountExcludingTax,
                    new AmountIncludingTax(amountExcludingTax, taxRate),
                    taxRate
                );

            await reader.Add(contract);
            var result = await reader.Delete(contract);

            Assert.IsAssignableFrom<IContractService<ContractModel>>(reader);
            await Assert.ThrowsAsync<NullReferenceException>(async () => await reader.GetByID(contract.ContractId));
            Assert.Equal(CSMS.GlobalEnum.GlobalEnum.DeleteResult.Success, result);
        }
    }
}
