using AutoFixture;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using InsightArchitectures.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test, CustomAutoData]
        public void AutoFixture_returns_TestContextLoggerAdapter_when_ILogger_is_requested([Frozen] ILogger<TestService> logger, TestService sut)
        {
            Assert.That(logger, Is.InstanceOf<TestContextLoggerAdapter<TestService>>());
        }

        [Test, CustomAutoData]
        public void TestService_can_be_tested_with_attribute(TestService sut, string name)
        {
            var result = sut.Hello(name);

            Assert.That(result, Does.EndWith(name));
        }

        [Test]
        public void TestService_can_be_tested()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new TypeRelay(typeof(ILogger<>), typeof(TestContextLoggerAdapter<>)));

            var sut = fixture.Create<TestService>();

            var name = fixture.Create<string>();

            var result = sut.Hello(name);

            Assert.That(result, Does.EndWith(name));
        }
    }
}
