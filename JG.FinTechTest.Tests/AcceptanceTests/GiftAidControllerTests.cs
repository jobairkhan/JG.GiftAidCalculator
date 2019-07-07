using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using JG.FinTechTest.GiftAid;
using Newtonsoft.Json;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.AcceptanceTests
{
    [TestFixture()]
    [SuppressMessage("ReSharper", "TestFileNameSpaceWarning")]
    public class GiftAidControllerTests
    {
        private ApiWebApplicationFactory _factory;

        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new ApiWebApplicationFactory();
            _httpClient = _factory.CreateClient();

            _httpClient
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();
            _factory?.Dispose();
        }

        [Test()]
        public async Task Test_should_return_ok()
        {
            var result = await _httpClient.GetAsync($"api/giftAid/ping", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test()]
        public async Task GET_should_return_ok()
        {
            const decimal donation = 100;
            var result = await _httpClient.GetAsync($"api/giftAid?amount={donation}", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GET_should_return_correct_value()
        {
            const decimal donationAmount = 100.00M;
            var giftAid = GetGiftAid(donationAmount);
            var expected = $"{{\"donationAmount\":{donationAmount},\"giftAidAmount\":{giftAid}}}";


            var response = await _httpClient.GetAsync($"api/giftAid?amount={donationAmount}", CancellationToken.None);

            var actual = await response.Content.ReadAsStringAsync();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task GET_Should_Return_error_when_amount_is_less_than_minimum()
        {
            decimal donationAmount = 0.00M;

            var response = await _httpClient.GetAsync($"api/giftAid?amount={donationAmount}", CancellationToken.None);

            var actual = await response.Content.ReadAsStringAsync();

            Assert.That(actual, Is.EqualTo("{\"amount\":[\"The field Amount must be between 2 and 100000.\"]}"));
        }

        [Test]
        public async Task POST_should_return_saved_Id_with_gift_aid()
        {
            var requestData = new DonationRequest
            {
                Name = "Mr. X",
                PostCode = "SE1 0TA",
                Amount = 100M
            };
            var content = CreateHttpContent(requestData);

            var response = await _httpClient.PostAsync($"api/giftAid", content);

            var actual = await response.Content.ReadAsStringAsync();
            StringAssert.StartsWith("{\"id\":", actual);

            var giftAid = GetGiftAid(requestData.Amount);
            StringAssert.EndsWith($"\"giftAidAmount\":{giftAid}}}", actual);
        }

        private static ByteArrayContent CreateHttpContent(DonationRequest requestData)
        {
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var buffer = Encoding.UTF8.GetBytes(jsonContent);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        private static decimal GetGiftAid(decimal donationAmount)
        {
            const decimal taxRate = 20M;
            var giftAid = donationAmount
                .GiftAidCalculation(taxRate)
                .Round2();
            return giftAid;
        }
    }
}
