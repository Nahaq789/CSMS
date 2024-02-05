using CSMS.Domain.Models;
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
            SetUpTask(context);
            context.SaveChanges();
            t.Commit();
        }
        private static void SetUpCustomers(ApplicationDbContext context)
        {
            var Customers = new CustomerModel(
                    "nahaq",
                    "nahaq@gmail.com",
                    23
                );
            context.Customers.Add(Customers);
        }

        private static void SetUpTask(ApplicationDbContext context)
        {
            var task = new TaskModel(
                    new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                    "task set up",
                    "task set up contents",
                    DateTime.Now.ToUniversalTime(),
                    new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                    new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                    1
                );

            context.Task.Add(task);
        }
    }
}
