using JG.FinTechTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace JG.FinTechTest.Tests.Controllers
{
    public class GiftAidControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Should_return_OK()
        {
            var sut = new GiftAidController();
            var actual = sut.Test();
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
        }
    }
}