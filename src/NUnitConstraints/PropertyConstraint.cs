using System;
using NUnit.Framework.Constraints;

namespace InsightArchitectures.Testing
{
    /// <summary>
    /// A NUnit constraint that applies a constraint to a property of the tested object.
    /// </summary>
    /// <typeparam name="T">The type of the tested object.</typeparam>
    public class PropertyConstraint<T> : Constraint
    {
        private readonly Func<T, object> _valueSelector;
        private readonly IConstraint _constraint;

        /// <summary>
        /// Creates an instance of <see cref="PropertyConstraint{T}" />.
        /// </summary>
        /// <param name="valueSelector">The property whose value will have the <paramref name="constraint"/> applied to.</param>
        /// <param name="constraint">The constraint to apply to the value of the property selected by <paramref name="valueSelector"/>.</param>
        public PropertyConstraint(Func<T, object> valueSelector, IConstraint constraint)
        {
            _valueSelector = valueSelector ?? throw new ArgumentNullException(nameof(valueSelector));
            _constraint = constraint ?? throw new ArgumentNullException(nameof(constraint));
        }

        /// <inheritdoc />
        public override ConstraintResult ApplyTo<TActual>(TActual actual)
        {
            if (actual is T item)
            {
                var value = _valueSelector(item);
                return _constraint.ApplyTo(value);
            }

            throw new NotSupportedException();
        }
    }
}
