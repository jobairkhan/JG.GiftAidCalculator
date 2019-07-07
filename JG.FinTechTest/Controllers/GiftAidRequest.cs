using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    public class GiftAidRequest
    {
        /// <summary>
        /// The amount donor wish to donate
        /// </summary>
        [FromQuery(Name = "amount")] 
        [Range(2.00, 100000.00)]
        public decimal Amount { get; set; }
    }
}