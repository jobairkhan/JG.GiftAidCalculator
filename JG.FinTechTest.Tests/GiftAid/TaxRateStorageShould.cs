using System;
using System.Collections.Generic;
using System.Text;
using JG.FinTechTest.GiftAid;
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
    }
}
