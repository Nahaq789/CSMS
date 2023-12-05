namespace CSMS.DTO.Contract;

public class ContractDto
{
    public Guid ContractId { get; set; }
    public string ContractName { get; set; }
    public string ContractCode { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Money { get; set; }
    public decimal AmountExcludingTax { get; set; }
    public decimal TaxRate { get; set; }
}