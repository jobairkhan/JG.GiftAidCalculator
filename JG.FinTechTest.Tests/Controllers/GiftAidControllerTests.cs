using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Should_return_OK()
        {
            var sut = new GiftAidController();
            var actual = sut.Test();
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GET_Should_return_OK()
        {
            var sut = new GiftAidController();
            decimal donationAmount = 100M;

            var actual = await sut.GetGiftAid(donationAmount, CancellationToken.None);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual?.Result, Is.InstanceOf<OkObjectResult>());
        }
    }
}