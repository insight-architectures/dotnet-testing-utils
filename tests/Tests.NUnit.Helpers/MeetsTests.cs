using NUnit.Framework;
using AutoFixture.Idioms;
using InsightArchitectures.Testing;
using NUnit.Framework.Constraints;

namespace Tests
{
    [TestFixture]
    public class MeetsTests
    {
        [Test, CustomAutoData]
        public void All_does_not_accept_null(GuardClauseAssertion assertion) => assertion.Verify(typeof(Meets).GetMethod(nameof(Meets.All)));

        [Test, CustomAutoData]
        public void All_returns_an_instance_of_AllConstraint(IConstraint[] constraints)
        {
            var result = Meets.All(constraints);

            Assert.That(result, Is.InstanceOf<AllConstraint>());
        }
    }
}
