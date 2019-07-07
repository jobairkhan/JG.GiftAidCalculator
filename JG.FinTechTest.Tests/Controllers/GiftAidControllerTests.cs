using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerTests
    {
        GiftAidController _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new GiftAidController();
        }

        [Test]
        public void Ping_Should_return_OK()
        {
            var actual = _sut.Test();
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task GET_Should_return_OK()
        {
            const decimal donationAmount = 100M;

            var actual = await _sut.GetGiftAid(donationAmount, CancellationToken.None);

            Assert.That(actual?.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GET_Should_return_CorrectValue()
        {
            const decimal donationAmount = 100M;
            const decimal taxRate = 20;
            decimal expected = donationAmount.GiftAidCalculation(taxRate).Round2();

            var act = await _sut.GetGiftAid(donationAmount, CancellationToken.None);


            dynamic actual = act?.Result;
            Assert.That(actual.Value, Is.EqualTo(expected));
        }
    }
}