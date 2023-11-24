using CSMS.Models.ValueObject;
using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CSMS.Models
{
    public class ContractModel
    {
        [Key]
        public Guid ContractId { get; private set; }
        [Required, NotNull, StringLength(50)]
        public string ContractName { get; private set; }
        [Required, NotNull, StringLength(50)]
        public string ContractCode { get; private set; }
        public Guid? CustomerId { get; private set; }
        public IMoneyModel Money { get; private set; }

    }
}
