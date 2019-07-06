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

        [Test]
        public void Return_zero_when_tax_rate_is_0()
        {
            const decimal taxRate = 0.0M;
            const decimal donationAmount = 0.0M;
            _taxRateStorage.CurrentRate.Returns(taxRate);

            var result = _sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
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
            return 0;
        }
    }
}
