using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Acceptance
{
    [TestFixture()]
    public class GiftAidControllerTests
    {
        private ApiWebApplicationFactory factory;

        private HttpClient HttpClientInstance { get; set; }

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            factory = new ApiWebApplicationFactory();
            HttpClientInstance = factory.CreateClient();

            HttpClientInstance
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            HttpClientInstance?.Dispose();
            factory?.Dispose();
        }

        [Test()]
        public async Task Test_should_return_ok()
        {
            var result = await HttpClientInstance.GetAsync($"api/giftAid/ping", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test()]
        public async Task GiftAid_should_return_ok()
        {
            decimal donation = 100;
            var result = await HttpClientInstance.GetAsync($"api/giftAid?amount={donation}", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test()]
        public async Task GiftAid_should_return_correct_value()
        {
            decimal donationAmount = 100.00M;
            decimal taxRate = 20M;
            var giftAid = donationAmount
                .GiftAidCalculation(taxRate)
                .Round2();
            var expected = $"{{\"donationAmount\":{donationAmount},\"giftAidAmount\":{giftAid}}}";


        var response = await HttpClientInstance.GetAsync($"api/giftAid?amount={donationAmount}", CancellationToken.None);

            var actual = await response.Content.ReadAsStringAsync();
            
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
