using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    public class GiftAidRequest
    {
        [FromQuery(Name = "amount")] 
        [Range(2.00, 100000.00)]
        public decimal Amount { get; set; }
    }
}