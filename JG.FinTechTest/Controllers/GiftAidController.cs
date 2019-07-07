using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Data;
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
        private readonly IRepository _repository;

        public GiftAidController(GiftAidCalculator calculator, IRepository repository)
        {
            _calculator = calculator;
            _repository = repository;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public Task<ActionResult<GiftAidResponse>> GetGiftAid([FromQuery]GiftAidRequest model, CancellationToken cancellation)
        {
            if (!ModelState.IsValid)
            {
                var error = BuildErrorMessage();
                return Task.FromResult<ActionResult<GiftAidResponse>>(BadRequest(error));
            }

            cancellation.ThrowIfCancellationRequested();

            var giftAid = _calculator.Calculate(model.Amount);

            var result = new GiftAidResponse(model.Amount, giftAid);

            return Task.FromResult<ActionResult<GiftAidResponse>>(Ok(result));
        }

        /// <summary>
        /// Persist donation data
        /// </summary>
        /// <param name="model"><see cref="DonationRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="DonationResponse"/></returns>
        /// TODO: Avoid returning for a post request
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<DonationResponse>> Donate(DonationRequest model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var error = BuildErrorMessage();
                return BadRequest(error);
            }

            cancellationToken.ThrowIfCancellationRequested();

            var giftAid = _calculator.Calculate(model.Amount);

            var donation = Donation.MakeNew(model.Name,
                                            model.PostCode,
                                            model.Amount,
                                            giftAid);
            var id = await _repository.Save(donation, cancellationToken);

            return Ok(new DonationResponse(id, giftAid));
        }

        private string BuildErrorMessage()
        {
            var valueErrors = ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToArray();

            var error = string.Join(", ", valueErrors);
            return error;
        }
    }
}
