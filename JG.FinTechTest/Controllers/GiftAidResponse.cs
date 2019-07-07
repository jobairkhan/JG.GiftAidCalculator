namespace JG.FinTechTest.Controllers
{
    public class GiftAidResponse
    {
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public decimal DonationAmount { get; }
        public decimal GiftAidAmount { get; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global

        public GiftAidResponse(decimal donationAmount, decimal giftAidAmount)
        {
            DonationAmount = donationAmount;
            GiftAidAmount = giftAidAmount;
        }
    }
}