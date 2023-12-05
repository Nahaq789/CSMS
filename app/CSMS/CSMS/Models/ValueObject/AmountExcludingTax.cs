using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

namespace CSMS.Models.ValueObject
{
    public class AmountExcludingTax
    {
        public decimal Value { get; private set; }
        public AmountExcludingTax() { }
        public AmountExcludingTax(decimal money) 
        {
            if (!IsValid(money)) throw new ArgumentOutOfRangeException();
            Value = money;
        }

        public AmountExcludingTax Add(AmountExcludingTax amountExcludingTax)
        {
            return new AmountExcludingTax(Value + amountExcludingTax.Value);
        }

        private static bool IsValid(decimal money)
        {
            return 0 <= money;
        }
    }
}
