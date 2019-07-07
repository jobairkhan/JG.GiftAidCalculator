namespace JG.FinTechTest.Controllers
{
    public class DonationResponse
    {
        public DonationResponse(int id, decimal giftAidAmount)
        {
            Id = id;
            GiftAidAmount = giftAidAmount;
        }

        /// <summary>
        /// Donation Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gift aid amount
        /// </summary>
        public decimal GiftAidAmount { get; }
    }
}