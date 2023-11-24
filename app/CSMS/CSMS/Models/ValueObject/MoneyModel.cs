using Microsoft.EntityFrameworkCore.Storage;

namespace CSMS.Models.ValueObject
{
    public interface IMoneyModel
    {
        public decimal CalcTaxMoney(decimal money, decimal tax);
    }
    public class MoneyModel : IMoneyModel
    {
        private decimal Money { get; }
        private decimal TaxMoney { get; }

        public MoneyModel(decimal money) 
        {
            this.Money = money;
        }
        public decimal CalcTaxMoney (decimal money, decimal tax)
        {
            MoneyModel _money = new MoneyModel(money);
            return _money.Money * tax;
        }
    }
}
