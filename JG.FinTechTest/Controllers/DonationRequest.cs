using System.ComponentModel.DataAnnotations;

namespace JG.FinTechTest.Controllers
{
    public class DonationRequest : GiftAidRequest
    {
        /// <summary>
        /// Donor's Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Donor's Postcode
        /// </summary>
        [Required]
        public string PostCode { get; set; }
        
    }
}