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
        public AmountIncludingTax TaxMoney { get; }
        public TaxRate TaxRate { get; }
        [NotMapped]
        // [JsonPropertyName("Money")]
        public decimal _Money { get; private set; }

        public ContractModel() { }
        [JsonConstructor]
        public ContractModel(
            Guid contractId,
            string contractName,
            string contractCode,
            Guid customerId,
            decimal _money
        )
        {
            this.ContractId = contractId;
            this.ContractName = contractName;
            this.ContractCode = contractCode;
            this.CustomerId = customerId;
            this._Money = _money;
            this.Money = new AmountExcludingTax(_money);
            this.TaxRate = new TaxRate();
            this.TaxMoney = new AmountIncludingTax(Money, TaxRate);
        }
        //public ContractModel(
        //    string contractName,
        //    string contractCode,
        //    Guid customerId,
        //    AmountExcludingTax money,
        //    AmountIncludingTax taxMoney,
        //    TaxRate taxRate
        //)
        //{
        //    this.ContractName = contractName;
        //    this.ContractCode = contractCode;
        //    this.CustomerId = customerId;
        //    this.Money = money;
        //    this.TaxMoney = taxMoney;
        //    this.TaxRate = taxRate;
        //}
    }
}
