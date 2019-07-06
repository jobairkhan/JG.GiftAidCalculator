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
            const decimal donationAmount = 2.0M;
            var sut = new GiftAidCalculator();

            var result = sut.Calculate(donationAmount);

            Assert.That(result, Is.EqualTo(0));
        }

    }

    public class GiftAidCalculator
    {
        public decimal Calculate(decimal donationAmount)
        {
            return 0;
        }
    }
}
