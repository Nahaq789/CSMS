using Microsoft.EntityFrameworkCore.Storage;

namespace CSMS.Models.ValueObject
{
    public interface IMoneyModel
    {
        public decimal CalcTaxMoney(decimal money, decimal tax);
        public decimal GetMoney();
    }
    public class MoneyModel : IMoneyModel
    {
        public decimal Money { get; private set; }
        public decimal TaxMoney { get; private set; }

        public MoneyModel(decimal money) 
        {
            this.Money = money;
        }
        public decimal CalcTaxMoney (decimal money, decimal tax)
        {
            MoneyModel _money = new MoneyModel(money);
            TaxMoney = _money.Money * tax;
            return TaxMoney;
        }

        public decimal GetMoney()
        {
            return Money;
        }
    }
}
