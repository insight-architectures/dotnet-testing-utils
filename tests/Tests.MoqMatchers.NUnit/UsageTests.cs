using InsightArchitectures.Testing;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class UsageTests
    {
        [Test, CustomAutoData]
        public void A(ISynchronousService sut, string parameter)
        {
            var actual = sut.Echo(parameter);

            var testConstraint = Is.EqualTo(parameter);

            Assume.That(parameter, testConstraint);

            Mock.Get(sut).Verify(p => p.Echo(Parameter.Is<string>().That(testConstraint)));
        }
    }
}
