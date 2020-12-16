using System;
using NUnit.Framework.Constraints;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A set of helpers that can be used when authoring NUnit-based tests.
    /// </summary>
    public static class Meets
    {
        /// <summary>
        /// Checks that all the underlying constraints are met.
        /// </summary>
        /// <param name="constraints">The constraints to meet.</param>
        /// <returns>An instance of <see cref="AllConstraint" />.</returns>
        public static IConstraint All(params IConstraint[] constraints)
        {
            _ = constraints ?? throw new ArgumentNullException(nameof(constraints));

            return new AllConstraint(constraints);
        }
    }
}
