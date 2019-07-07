namespace JG.FinTechTest.GiftAid
{
    public class GiftAidCalculator
    {
        private readonly IStoreTaxRate _taxRateStorage;

        public GiftAidCalculator(IStoreTaxRate taxRateStorage)
        {
            _taxRateStorage = taxRateStorage;
        }

        public decimal Calculate(decimal donationAmount) => 
            donationAmount.GiftAidCalculation(_taxRateStorage.CurrentRate).Round2();
    }
}
