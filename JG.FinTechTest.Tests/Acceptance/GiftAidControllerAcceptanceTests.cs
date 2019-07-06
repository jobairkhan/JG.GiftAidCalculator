using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Acceptance
{
    internal class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
    }

    [TestFixture]
    public class GiftAidControllerAcceptanceTests
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

        [Test]
        public async Task Test_should_return_ok()
        {
            var result = await HttpClientInstance.GetAsync($"api/giftaid", CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
