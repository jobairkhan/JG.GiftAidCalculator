using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerTests
    {
        GiftAidController _sut;
        IStoreTaxRate _taxRateStorage;

        [SetUp]
        public void Setup()
        {
            _taxRateStorage = Substitute.For<IStoreTaxRate>();
            GiftAidCalculator calc = new GiftAidCalculator(_taxRateStorage);
            _sut = new GiftAidController(calc);
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
            var expected = donationAmount.GiftAidCalculation(taxRate).Round2();
            _taxRateStorage.CurrentRate.Returns(20);

            var act = await _sut.GetGiftAid(donationAmount, CancellationToken.None);


            dynamic actual = act?.Result;
            Assert.That(actual?.Value?.GiftAidAmount, Is.EqualTo(expected));
        }
    }
}