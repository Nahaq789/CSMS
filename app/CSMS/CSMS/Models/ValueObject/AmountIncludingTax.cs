using System.ComponentModel.DataAnnotations.Schema;

namespace CSMS.Models.ValueObject
{
    public class AmountIncludingTax
    {
        [NotMapped]
        private readonly decimal TaxMoney;
        public decimal Value { get { return TaxMoney; } private set { } }
        public AmountIncludingTax() { }
        public AmountIncludingTax(AmountExcludingTax amountExcludingTax, TaxRate taxRate)
        {
            TaxMoney = amountExcludingTax.Value * (1 + (taxRate.Value / 100));
        }
    }
}
