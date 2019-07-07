using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    public class GiftAidRequest
    {
        [FromQuery(Name = "amount")] 
        public decimal Amount { get; set; }
    }
}