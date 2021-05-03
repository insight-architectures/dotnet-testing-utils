using InsightArchitectures.Testing;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tests
{
    [TestFixture]
    [TestOf(typeof(ParameterExtensions))]
    public class ParameterExtensionsTests
    {
        [Test, CustomAutoData]
        public void That_returns_true_if_expectation_is_met(Parameter<string> parameter, IConstraint constraint)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            var mock = new Mock<ISynchronousService>();

            mock.Setup(p => p.Echo(It.IsAny<string>())).Returns("world");

            var service = mock.Object;

            var actual = service.Echo("hello world");

            mock.Verify(p => p.Echo(ParameterExtensions.That(parameter, constraint)));
        }

        [Test, CustomAutoData]
        public void That_returns_false_if_expectation_is_not_met(Parameter<string> parameter, IConstraint constraint)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            var mock = new Mock<ISynchronousService>();

            mock.Setup(p => p.Echo(It.IsAny<string>())).Returns("world");

            var service = mock.Object;

            var actual = service.Echo("hello world");

            Assert.That(() => mock.Verify(p => p.Echo(ParameterExtensions.That(parameter, constraint))), Throws.InstanceOf<MockException>());
        }

        [Test, CustomAutoData]
        public void That_returns_true_if_expectation_is_met_by_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            mock.Verify(p => p.DoSomething(ParameterExtensions.That(parameter, p => p.StringProperty, constraint)));

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void That_returns_true_if_expectation_is_not_met_by_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            Assert.That(() => mock.Verify(p => p.DoSomething(ParameterExtensions.That(parameter, p => p.StringProperty, constraint))), Throws.InstanceOf<MockException>());

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void That_returns_true_if_expectation_is_met_by_async_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            mock.Verify(p => p.DoSomething(ParameterExtensions.That(parameter, p => p.GetString(), constraint)));

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void That_returns_true_if_expectation_is_not_met_by_async_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            Assert.That(() => mock.Verify(p => p.DoSomething(ParameterExtensions.That(parameter, p => p.GetString(), constraint))), Throws.InstanceOf<MockException>());

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void Assert_returns_true_if_expectation_is_met(Parameter<string> parameter, IConstraint constraint)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<ISynchronousService>();

            mock.Setup(p => p.Echo(It.IsAny<string>())).Returns("world");

            var service = mock.Object;

            var actual = service.Echo("hello world");

            mock.Verify(p => p.Echo(ParameterExtensions.Assert(parameter, constraint)));
        }

        [Test, CustomAutoData]
        public void Assert_returns_false_if_expectation_is_not_met(Parameter<string> parameter, IConstraint constraint)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<ISynchronousService>();

            mock.Setup(p => p.Echo(It.IsAny<string>())).Returns("world");

            var service = mock.Object;

            var actual = service.Echo("hello world");

            Assert.That(() => mock.Verify(p => p.Echo(ParameterExtensions.Assert(parameter, constraint))), Throws.InstanceOf<AssertionException>());
        }

        [Test, CustomAutoData]
        public void Assert_returns_true_if_expectation_is_met_by_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            mock.Verify(p => p.DoSomething(ParameterExtensions.Assert(parameter, p => p.StringProperty, constraint)));

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void Assert_returns_true_if_expectation_is_not_met_by_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            Assert.That(() => mock.Verify(p => p.DoSomething(ParameterExtensions.Assert(parameter, p => p.StringProperty, constraint))), Throws.InstanceOf<AssertionException>());

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void Assert_returns_true_if_expectation_is_met_by_async_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, true));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            mock.Verify(p => p.DoSomething(ParameterExtensions.Assert(parameter, p => p.GetString(), constraint)));

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }

        [Test, CustomAutoData]
        public void Assert_returns_true_if_expectation_is_not_met_by_async_selected_value(Parameter<CompositeParameter> parameter, IConstraint constraint, CompositeParameter testParameter)
        {
            Mock.Get(constraint).Setup(p => p.ApplyTo(It.IsAny<string>())).Returns((string value) => new ConstraintResult(constraint, value, false));

            Mock.Get(constraint).Setup(p => p.Resolve()).Returns(constraint);

            var mock = new Mock<IServiceWithCompositeParameter>();

            mock.Setup(p => p.DoSomething(It.IsAny<CompositeParameter>()));

            var service = mock.Object;

            service.DoSomething(testParameter);

            Assert.That(() => mock.Verify(p => p.DoSomething(ParameterExtensions.Assert(parameter, p => p.GetString(), constraint))), Throws.InstanceOf<AssertionException>());

            Mock.Get(constraint).Verify(p => p.ApplyTo(testParameter.StringProperty));
        }
    }
}
