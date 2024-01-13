using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CSMS.Domain.Models.ValueObject;
using Newtonsoft.Json;

namespace CSMS.Domain.Models;

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

    public EstimateModel() { }

    [JsonConstructor]
    public EstimateModel(
        Guid estimateId,
        string estimateCode,
        string estimateName,
        decimal _money
    )
    {
        EstimateId = estimateId;
        EstimateCode = estimateCode;
        EstimateName = estimateName;
        _Money = _money;
        AmountExcludingTax = new AmountExcludingTax(_Money);
        TaxRate = new TaxRate();
        AmountIncludingTax = new AmountIncludingTax(AmountExcludingTax, TaxRate);
    }
}