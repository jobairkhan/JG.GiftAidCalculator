﻿using JG.FinTechTest.GiftAid;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.GiftAid
{
    [TestFixture]
    public class TaxRateStorageShould
    {
        [Test]
        public void Implement_interface()
        {
            var sut = new TaxRateStorage();

            Assert.That(sut, Is.InstanceOf<IStoreTaxRate>());
        }

        [Test]
        public void Default_rate_is_20()
        {
            var sut = new TaxRateStorage();

            Assert.That(sut.CurrentRate, Is.EqualTo(20M));
        }
    }
}
