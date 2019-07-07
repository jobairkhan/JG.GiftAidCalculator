using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using JG.FinTechTest.Data;
using JG.FinTechTest.GiftAid;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Controllers
{
    [TestFixture]
    public class GiftAidControllerTests
    {
        GiftAidController _sut;
        IStoreTaxRate _taxRateStorage;
        private IRepository _repository;

        [SetUp]
        public void Setup()
        {
            _taxRateStorage = Substitute.For<IStoreTaxRate>();
            _repository = Substitute.For<IRepository>();
            GiftAidCalculator calc = new GiftAidCalculator(_taxRateStorage);
            _sut = new GiftAidController(calc, _repository);
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

            var actual = await _sut.GetGiftAid(MakeGiftAidRequest(donationAmount), CancellationToken.None);

            Assert.That(actual?.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GET_Should_return_CorrectValue()
        {
            const decimal taxRate = 20;
            var donation = MakeGiftAidRequest(100M);
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
            var donation = MakeGiftAidRequest(donationAmount);
            _taxRateStorage.CurrentRate.Returns(20);

            var act = await _sut.GetGiftAid(donation, CancellationToken.None);


            dynamic actual = act?.Result;
            Assert.That(actual?.Value?.DonationAmount, Is.EqualTo(donationAmount));
        }

        [Test]
        public async Task GET_Should_return_bad_request_when_invalid_state()
        {
            var donationAmount = 1M;
            var donation = MakeGiftAidRequest(donationAmount);
            _sut.ModelState.AddModelError("Amount", "Invalid Amount");

            var actual = await _sut.GetGiftAid(donation, CancellationToken.None);

            Assert.That(actual?.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void Should_ThrowIfCancellationRequested()
        {
            var donation = MakeGiftAidRequest(10);
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
            var donation = MakeGiftAidRequest(donationAmount);
            _sut.ModelState.AddModelError("Amount", "Invalid Amount");
            _sut.ModelState.AddModelError("Amount", "Amount cannot be less than 2");

            var actual = await _sut.GetGiftAid(donation, CancellationToken.None);

            var actualResult = actual?.Result as BadRequestObjectResult;
            Assert.That(actualResult?.Value, Is.EqualTo("Invalid Amount, Amount cannot be less than 2"));
        }

        [Test]
        public async Task POST_Should_return_id()
        {
            var donationAmount = 100M;
            var donation = new DonationRequest
            {
                Name = "X",
                PostCode = "PC",
                Amount = donationAmount
            };
            _taxRateStorage.CurrentRate.Returns(20);

            var act = await _sut.Donate(donation, CancellationToken.None);

            var actual = act?.Value as DonationResponse;
            Assert.That(actual?.Id, Is.EqualTo(1));
        }

        [TestCase(2)]
        public async Task POST_Should_return_correct_id(int expectedId)
        {
            var donationAmount = 100M;
            var donation = new DonationRequest
            {
                Name = "X",
                PostCode = "PC",
                Amount = donationAmount
            };
            _taxRateStorage.CurrentRate.Returns(20);
            _repository.Save(Arg.Any<Donation>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(expectedId);

            var act = await _sut.Donate(donation, CancellationToken.None);

            var actual = act?.Value as DonationResponse;
            Assert.That(actual?.Id, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task POST_Should_return_gift_aid_amount()
        {
            const decimal rate = 20;
            const decimal donationAmount = 100M;
            var donation = MakeDonationRequest(donationAmount);
            _taxRateStorage.CurrentRate.Returns(rate);

            var act = await _sut.Donate(donation, CancellationToken.None);

            var expected = donationAmount.GiftAidCalculation(rate).Round2();
            Assert.That(act?.Value?.GiftAid, Is.EqualTo(expected));
        }

        [Test]
        public async Task POST_Should_call_save()
        {
            const decimal rate = 20;
            const decimal donationAmount = 100M;
            var donationReq = MakeDonationRequest(donationAmount);
            _taxRateStorage.CurrentRate.Returns(rate);

            await _sut.Donate(donationReq, CancellationToken.None);

            var donation = Donation.MakeNew(donationReq.Name,
                                        donationReq.PostCode,
                                        donationReq.Amount,
                                        0);
            _repository.Received(1).Save(donation, CancellationToken.None);
        }

        private static DonationRequest MakeDonationRequest(decimal donationAmount)
        {
            return new DonationRequest
            {
                Name = "X",
                PostCode = "PC",
                Amount = donationAmount
            };
        }

        [Test]
        public async Task POST_Should_return_bad_request_when_invalid_state()
        {
            var donationAmount = 1M;
            var donation = MakeDonationRequest(donationAmount);
            _sut.ModelState.AddModelError("Amount", "Invalid Amount");

            var actual = await _sut.Donate(donation, CancellationToken.None);

            Assert.That(actual?.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        private GiftAidRequest MakeGiftAidRequest(decimal donationAmount)
        {
            return new GiftAidRequest()
            {
                Amount = donationAmount
            };
        }
    }
}