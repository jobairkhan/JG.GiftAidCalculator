using System.IO.Compression;
using System.Linq;
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
        /// <param name="model"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        public Task<ActionResult<GiftAidResponse>> GetGiftAid([FromQuery]GiftAidRequest model, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                var valueErrors = ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage)
                                            .ToArray();

                var error = string.Join(", ", valueErrors);
                return Task.FromResult<ActionResult<GiftAidResponse>>(BadRequest(error));
            }
            var giftAid = _calculator.Calculate(model.Amount);

            cancellation.ThrowIfCancellationRequested();

            var result = new GiftAidResponse(model.Amount, giftAid);

            return Task.FromResult<ActionResult<GiftAidResponse>>(Ok(result));
        }
    }
}
