using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Is_returns_instance_of_Parameter()
        {
            var parameter = Parameter.Is<string>();

            Assert.That(parameter, Is.InstanceOf<Parameter<string>>());
        }
    }
}
