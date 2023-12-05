using CSMS.Models.ValueObject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using JsonConstructorAttribute = System.Text.Json.Serialization.JsonConstructorAttribute;

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
        public Guid CustomerId { get; private set; }
        public AmountExcludingTax Money { get; }
        public TaxRate TaxRate { get; }
        
        [NotMapped]
        public decimal _Money { get; private set; }
        [NotMapped]
        public decimal _TaxRate { get; private set; }

        public ContractModel() { }
        public ContractModel(
            Guid contractId,
            string contractName,
            string contractCode,
            Guid customerId,
            decimal _money,
            decimal _taxRate
        )
        {
            this.ContractId = contractId;
            this.ContractName = contractName;
            this.ContractCode = contractCode;
            this.CustomerId = customerId;
            this._Money = _money;
            this._TaxRate = _taxRate;
            this.Money = new AmountExcludingTax(_money);
            this.TaxRate = new TaxRate(_taxRate);
        }
    }
}
