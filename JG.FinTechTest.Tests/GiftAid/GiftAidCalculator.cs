using NUnit.Framework;
using NSubstitute;

namespace JG.FinTechTest.Tests.GiftAid
{
    [TestFixture]
    public class GiftAidCalculatorShould
    {
        GiftAidCalculator _sut;
        private IStoreTaxRate _taxRateStorage;

        [SetUp]
        public void Setup()
        {
            _taxRateStorage = Substitute.For<IStoreTaxRate>();
            _sut = new GiftAidCalculator(_taxRateStorage);
        }

        [Test]
        public void Return_zero_when_donation_amount_is_0()
        {
            const decimal donationAmount = 0.0M;

            var result = _sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
        }

        [TestCase(0.0, 0.0)]
        [TestCase(0.0, 1.0)]
        [TestCase(0.0, 100.0)]
        public void Return_zero_when_tax_rate_is_0(decimal taxRate, decimal donationAmount)
        {
            _taxRateStorage.CurrentRate.Returns(taxRate);

            var result = _sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test()]
        public void Get_read_current_tax_rate()
        {
            var returnThisTaxRate = 1;
            _taxRateStorage.CurrentRate.Returns(returnThisTaxRate);

            _sut.Calculate(1);

            var unused = _taxRateStorage.Received().CurrentRate;
        }

        [TestCase(20.0, 1.0, 0.25)]
        public void Return_correct_gift_aid(decimal taxRate, decimal donationAmount, decimal expectedGiftAid)
        {
            _taxRateStorage.CurrentRate.Returns(taxRate);

            var result = _sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(expectedGiftAid));
        }
    }

    public interface IStoreTaxRate
    {
        decimal CurrentRate { get; }
    }

    public class GiftAidCalculator
    {
        private readonly IStoreTaxRate _taxRateStorage;

        public GiftAidCalculator(IStoreTaxRate taxRateStorage)
        {
            _taxRateStorage = taxRateStorage;
        }

        public decimal Calculate(decimal donationAmount)
        {
            var taxRate = _taxRateStorage.CurrentRate;
            return donationAmount * (taxRate / (100 - taxRate));
        }
    }
}
