using CSMS.Models.ValueObject;
using System;
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

        public AmountExcludingTax Money { get; private set; }
        public AmountIncludingTax TaxMoney { get; private set; }
        public TaxRate TaxRate { get; private set; }

        //public MoneyModel TaxMoney { get; private set; }

        private ContractModel()
        {
        }

        public ContractModel(
            Guid contractId,
            string contractName,
            string contractCode,
            Guid customerId,
            AmountExcludingTax money,
            AmountIncludingTax taxMoney,
            TaxRate taxRate
        )
        {
            this.ContractId = contractId;
            this.ContractName = contractName;
            this.ContractCode = contractCode;
            this.CustomerId = customerId;
            this.Money = money;
            this.TaxMoney = taxMoney;
            this.TaxRate = taxRate;
        }

        public ContractModel(
            string contractName,
            string contractCode,
            Guid customerId,
            AmountExcludingTax money,
            AmountIncludingTax taxMoney,
            TaxRate taxRate
        )
        {
            this.ContractName = contractName;
            this.ContractCode = contractCode;
            this.CustomerId = customerId;
            this.Money = money;
            this.TaxMoney = taxMoney;
            this.TaxRate = taxRate;
        }
    }
}
