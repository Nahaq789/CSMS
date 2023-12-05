using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CSMS.Models.ValueObject
{
    public class TaxRate
    {
        public decimal Value { get; private set; }
        public TaxRate() { }
        
        public TaxRate(decimal rate) 
        {
            if(!IsValid(rate))
            {
                throw new ArgumentException();
            }
            Value = rate;
        }
        private static bool IsValid(decimal rate)
        {
            return 0 <= rate;
        }
    }
}
