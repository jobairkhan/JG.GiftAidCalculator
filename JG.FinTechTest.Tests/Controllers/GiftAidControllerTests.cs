using System;
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

            var actual = await _sut.GetGiftAid(MakeRequest(donationAmount), CancellationToken.None);

            Assert.That(actual?.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GET_Should_return_CorrectValue()
        {
            const decimal taxRate = 20;
            var donation = MakeRequest(100M);
            var expected = donation.Amount.GiftAidCalculation(taxRate).Round2();
            _taxRateStorage.CurrentRate.Returns(20);

            var act = await _sut.GetGiftAid(donation, CancellationToken.None);


            dynamic actual = act?.Result;
            Assert.That(actual?.Value?.GiftAidAmount, Is.EqualTo(expected));
        }

        [Test]
        public async Task GET_Should_return_correct_amount()
        {
            var donationAmount = 100M;
            var donation = MakeRequest(donationAmount);
            _taxRateStorage.CurrentRate.Returns(20);

            var act = await _sut.GetGiftAid(donation, CancellationToken.None);


            dynamic actual = act?.Result;
            Assert.That(actual?.Value?.DonationAmount, Is.EqualTo(donationAmount));
        }

        [Test]
        public async Task GET_Should_return_bad_request_when_invalid_state()
        {
            var donationAmount = 1M;
            var donation = MakeRequest(donationAmount);
            _sut.ModelState.AddModelError("Amount", "Invalid Amount");

            var actual = await _sut.GetGiftAid(donation, CancellationToken.None);
            
            Assert.That(actual?.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Should_ThrowIfCancellationRequested()
        {
            var donation = MakeRequest(10);
            var cts = new CancellationTokenSource();
            cts.Cancel();
            var cancellationToken = cts.Token;

            Assert.ThrowsAsync<OperationCanceledException>(async () =>
                    await _sut.GetGiftAid(donation, cancellationToken)
            );
        }

        [Test]
        public async Task GET_Should_return_all_errors_when_multiple_errors()
        {
            var donationAmount = 1M;
            var donation = MakeRequest(donationAmount);
            _sut.ModelState.AddModelError("Amount", "Invalid Amount");
            _sut.ModelState.AddModelError("Amount", "Amount cannot be less than 2");

            var actual = await _sut.GetGiftAid(donation, CancellationToken.None);

            var actualResult = actual?.Result as BadRequestObjectResult;
            Assert.That(actualResult?.Value, Is.EqualTo("Invalid Amount, Amount cannot be less than 2"));
        }

        private GiftAidRequest MakeRequest(decimal donationAmount)
        {
            return new GiftAidRequest()
            {
                Amount = donationAmount
            };
        }
    }
}