using NUnit.Framework;
using AutoFixture.Idioms;
using InsightArchitectures.Testing;
using NUnit.Framework.Constraints;
using Moq;

namespace Tests
{
    [TestFixture]
    public class AllConstraintTests
    {
        [Test, CustomAutoData]
        public void Constructor_does_not_accept_nulls(GuardClauseAssertion assertion) => assertion.Verify(typeof(AllConstraint).GetConstructors());

        [Test]
        public void Assert_succeeds_if_all_constraints_are_met()
        {
            const string TestValue = "Hello World";

            var sut = new AllConstraint(
                Does.StartWith("hello").IgnoreCase,
                Does.EndWith("world").IgnoreCase
            );

            Assert.That(TestValue, sut);
        }

        [Test, CustomAutoData]
        public void Assert_succeeds_if_all_constraints_are_met(object testValue, IConstraint[] constraints)
        {
            foreach (var constraint in constraints)
            {
                Mock.Get(constraint).Setup(p => p.ApplyTo(testValue)).Returns(new ConstraintResult(constraint, testValue, ConstraintStatus.Success));
            }

            var sut = new AllConstraint(constraints);

            Assert.That(testValue, sut);
        }

        [Test, CustomAutoData]
        public void Assert_checks_all_constraints(object testValue, IConstraint[] constraints)
        {
            foreach (var constraint in constraints)
            {
                Mock.Get(constraint).Setup(p => p.ApplyTo(testValue)).Returns(new ConstraintResult(constraint, testValue, ConstraintStatus.Success)).Verifiable();
            }

            var sut = new AllConstraint(constraints);

            Assert.That(testValue, sut);

            foreach (var constraint in constraints)
            {
                Mock.Get(constraint).Verify(p => p.ApplyTo(testValue));
            }
        }

        [Test, CustomAutoData]
        public void Assert_fails_if_any_constraint_fails()
        {
            const string TestValue = "Hello World";

            var sut = new AllConstraint(
                Does.StartWith("foo").IgnoreCase, // will fail
                Does.EndWith("world").IgnoreCase
            );

            Assert.That(() => Assert.That(TestValue, sut), Throws.Exception);
        }
    }
}
