using System;
using NUnit.Framework.Constraints;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A NUnit constraint that expects all the underlying constraints are met.
    /// </summary>
    public class AllConstraint : Constraint
    {
        private readonly IConstraint[] _constraints;

        /// <summary>
        /// Creates an instance of <see cref="AllConstraint"/>.
        /// </summary>
        /// <param name="constraints">The list of constraints to meet.</param>
        public AllConstraint(params IConstraint[] constraints)
        {
            _constraints = constraints ?? throw new ArgumentNullException(nameof(constraints));
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            var constraint = _constraints[0];

            for (var i = 1; i < _constraints.Length; i++)
            {
                constraint = new AndConstraint(constraint, _constraints[i]);
            }

            return constraint.ApplyTo(actual);
        }
    }
}
