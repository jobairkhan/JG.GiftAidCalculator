using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    [Route("api/giftaid")]
    [ApiController]
    public class GiftAidController : ControllerBase
    {
        /// <summary>
        /// Ping
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ping")]
        public IActionResult Test()
        {
            return Ok("Hello World");
        }

        public async Task<ActionResult<decimal>> GetGiftAid(decimal donationAmount, CancellationToken cancellation)
        {
            return Ok(0M);
        }
    }
}
