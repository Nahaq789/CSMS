using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSMS.Models.ValueObject
{
    public class AmountExcludingTax
    {
        [NotMapped]
        private readonly decimal Money;
        public decimal Value { get { return Money; } private set { } }
        private AmountExcludingTax() { }
        public AmountExcludingTax(decimal money) 
        {
            if (IsValid(money)) throw new ArgumentOutOfRangeException();
            Money = money;
        }

        public AmountExcludingTax Add(AmountExcludingTax amountExcludingTax)
        {
            return new AmountExcludingTax(Money + amountExcludingTax.Money);
        }

        private static bool IsValid(decimal money)
        {
            return 0 <= money;
        }
    }
}
