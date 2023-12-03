using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CSMS.Models.ValueObject;
using Newtonsoft.Json;

namespace CSMS.Models;

public class EstimateModel
{
    [Key]
    public Guid EstimateId { get; private set; }
    [Required, NotNull, StringLength(10)]
    public string EstimateCode { get; private set; }
    [Required, NotNull, StringLength(30)]
    public string EstimateName { get; private set; }
    [NotMapped]
    public decimal _Money { get; private set; }
    public AmountExcludingTax AmountExcludingTax { get; }
    public AmountIncludingTax AmountIncludingTax { get; }
    public TaxRate TaxRate { get; }

    public EstimateModel() {}
    
    [JsonConstructor]
    public EstimateModel(
        Guid estimateId,
        string estimateCode,
        string estimateName,
        decimal _money
    )
    {
        this.EstimateId = estimateId;
        this.EstimateCode = estimateCode;
        this.EstimateName = estimateName;
        this._Money = _money;
        this.AmountExcludingTax = new AmountExcludingTax(_Money);
        this.TaxRate = new TaxRate();
        this.AmountIncludingTax = new AmountIncludingTax(AmountExcludingTax, TaxRate);
    }
}