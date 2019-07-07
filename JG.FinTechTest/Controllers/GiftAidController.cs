using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Http;
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Test()
        {
            return Ok("Hello World");
        }

        /// <summary>
        /// Get the amount of gift aid reclaimable for donation amount
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ActionResult<GiftAidResponse>> GetGiftAid([FromQuery] decimal amount,
                                                                    CancellationToken cancellation)
        {
            var giftAid = _calculator.Calculate(amount);

            var result = new  GiftAidResponse(amount, giftAid);

            return Task.FromResult<ActionResult<GiftAidResponse>>(Ok(result));
        }
    }
}
