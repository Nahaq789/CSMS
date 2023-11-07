using CSMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCSMS.DB
{
    internal class GlobalSeed
    {
        public static void SetSeeds(ApplicationDbContext context)
        {
            using var t = context.Database.BeginTransaction();
            SetUpCustomers(context);
            context.SaveChanges();
            t.Commit();
        }
        private static void SetUpCustomers(ApplicationDbContext context)
        {
            var Customers = new CustomerModel(
                    Guid.NewGuid(),
                    "nahaq",
                    "nahaq@gmail.com",
                    23
                );
            context.Customers.Add(Customers);
        }
    }
}
