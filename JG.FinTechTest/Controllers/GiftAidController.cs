using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    [Route("api/giftaid")]
    [ApiController]
    public class GiftAidController : ControllerBase
    {
        private readonly GiftAidCalculator _calculator;

        public GiftAidController(GiftAidCalculator calculator)
        {
            _calculator = calculator;
        }

        /// <summary>
        /// Ping
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ping")]
        public IActionResult Test()
        {
            return Ok("Hello World");
        }

        [HttpGet, Route("{donation}")]
        public async Task<ActionResult<decimal>> GetGiftAid([FromRoute]decimal donation, CancellationToken cancellation)
        {
            var result = _calculator.Calculate(donation);
            return Ok(result);
        }
    }
}
