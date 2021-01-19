using System;
using AutoFixture.Idioms;
using InsightArchitectures.Testing;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(PropertyConstraint<>))]
    public class PropertyConstraintTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(PropertyConstraint<>).GetConstructors());

        [Test, CustomAutoData]
        public void Assert_succeeds(CompositeType test)
        {
            var sut = new PropertyConstraint<CompositeType>(ct => ct.StringValue, Is.EqualTo(test.StringValue));

            Assert.That(test, sut);
        }

        [Test, CustomAutoData]
        public void ApplyTo_returns_success_when_expected(CompositeType test)
        {
            var sut = new PropertyConstraint<CompositeType>(ct => ct.StringValue, Is.EqualTo(test.StringValue));

            var result = sut.ApplyTo(test);

            Assert.That(result.IsSuccess, Is.True);
        }

        [Test, CustomAutoData]
        public void ApplyTo_throws_not_supported_if_test_is_not_same_as_type(object test)
        {
            var sut = new PropertyConstraint<CompositeType>(ct => ct.StringValue, Is.EqualTo("Hello world"));

            Assert.That(() => sut.ApplyTo(test), Throws.Exception.TypeOf<NotSupportedException>());
        }
    }
}
