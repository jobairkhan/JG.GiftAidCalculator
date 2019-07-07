using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
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
            var result = await HttpClientInstance.GetAsync($"api/giftAid", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
