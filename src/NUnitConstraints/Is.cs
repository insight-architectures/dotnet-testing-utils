using System;
using NUnit.Framework.Constraints;

namespace InsightArchitectures.Testing
{
#pragma warning disable CA1716 // Identifiers should not match keywords
#pragma warning disable CA1000 // Do not declare static members on generic types
    /// <summary>
    /// A set of helpers that can be used when authoring NUnit-based tests.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the test subject.</typeparam>
    public static class Is<T>
    {
        /// <summary>
        /// Selects a property of the test subject and encapsulates it within a <see cref="IConstraintBuilder" />.
        /// </summary>
        /// <param name="selector">The property to be tested.</param>
        /// <returns>An instance of <see cref="IConstraintBuilder" /> containing the reference to the selected property.</returns>
        public static IConstraintBuilder With(Func<T, object> selector)
        {
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            return new PropertySelectorConstraintBuilder<T>(selector);
        }
    }
#pragma warning restore CA1716 // Identifiers should not match keywords
#pragma warning restore CA1000 // Do not declare static members on generic types
}
