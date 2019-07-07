namespace JG.FinTechTest.GiftAid
{
    public class TaxRateStorage : IStoreTaxRate
    {
        public TaxRateStorage()
        {
            CurrentRate = 20M;
        }
        public decimal CurrentRate { get; }
    }
}