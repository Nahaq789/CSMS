using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CSMS.Models.ValueObject
{
    public class TaxRate
    {
        [NotMapped]
        private readonly decimal Rate;
        public decimal Value { get { return Rate; } private set { } }
        public TaxRate() { }
        
        public TaxRate(decimal rate) 
        {
            if(!IsValid(rate))
            {
                throw new ArgumentException();
            }
            Rate = rate;
        }
        private static bool IsValid(decimal rate)
        {
            return 0 <= rate;
        }
    }
}
