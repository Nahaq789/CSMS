using CSMS.Domain.DomainService;
using CSMS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSMS.ServiceTest
{
    public class TestContractService : IClassFixture<TestContractService>
    {
        private ApplicationDbContext _context;
        public TestContractService(ApplicationDbContext context)
        {
            _context = context;
        }

        [Fact]
        public async void GetByID()
        {
            //var reader = new ContractService(_context);
            //ContractModel contract = new ContractModel(
            //        "nahaContract",
            //        "111-789",
            //        Guid.Empty
            //    );
        }
    }
}
