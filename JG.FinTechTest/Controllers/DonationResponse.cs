namespace JG.FinTechTest.Controllers
{
    public class DonationResponse
    {
        public DonationResponse(int id, decimal giftAid)
        {
            Id = id;
            GiftAid = giftAid;
        }

        /// <summary>
        /// Donation Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gift aid amount
        /// </summary>
        public decimal GiftAid { get; }
    }
}