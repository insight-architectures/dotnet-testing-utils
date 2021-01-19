using NUnit.Framework.Constraints;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// Helper interface to fluently compose constraints.
    /// </summary>
    public interface IConstraintBuilder
    {
        /// <summary>
        /// Wraps the given <paramref name="constraint"/> into another <see cref="IConstraint"/>.
        /// </summary>
        /// <param name="constraint">The <see cref="IConstraint" /> to wrap.</param>
        /// <returns>A wrapped constraint.</returns>
        IConstraint That(IConstraint constraint);
    }
}
