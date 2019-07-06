using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework.Constraints;

namespace JG.FinTechTest.Tests.GiftAid
{
    [TestFixture]
    public class GiftAidCalculatorShould
    {
        [Test]
        public void Return_zero_when_donation_amount_is_0()
        {
            const decimal donationAmount = 0.0M;
            var sut = new GiftAidCalculator(0);

            var result = sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Return_zero_when_tax_rate_is_0()
        {
            const decimal taxRate = 0.0M;
            const decimal donationAmount = 0.0M;
            var sut = new GiftAidCalculator(taxRate);

            var result = sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
        }
    }

    public class GiftAidCalculator
    {
        public GiftAidCalculator(decimal taxRate)
        {
            
        }

        public decimal Calculate(decimal donationAmount)
        {
            return 0;
        }
    }
}
