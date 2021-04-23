#region Using namespaces

using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

#nullable enable
namespace FoundersPC.ApplicationShared.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ShouldBeInAttribute : ValidationAttribute
    {
        private readonly Type _type;
        private readonly IEnumerable _values;

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="type"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.OutOfMemoryException">
        ///     The length of the resulting string overflows the maximum allowed length
        ///     (<see cref="F:System.Int32.MaxValue"/>).
        /// </exception>
        public ShouldBeInAttribute(Type type, IEnumerable values) : base($"Value should be in {String.Join(',', values)}")
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _values = values ?? throw new ArgumentNullException(nameof(values));
        }

        /// <inheritdoc/>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="values"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="type"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.OutOfMemoryException">
        ///     The length of the resulting string overflows the maximum allowed length
        ///     (<see cref="F:System.Int32.MaxValue"/>).
        /// </exception>
        public ShouldBeInAttribute(Type type, params object[] values) : this(type, values.AsEnumerable()) { }

        #region Overrides of ValidationAttribute

        /// <inheritdoc/>
        public override bool IsValid(object? value)
        {
            try
            {
                return _values.Cast<object>()
                              .Any(paramValue => value?.Equals(paramValue) ?? false);
            }
            catch (InvalidCastException)
            {
                // TODO: Handle the System.InvalidCastException
            }
            catch (ArgumentNullException)
            {
                // TODO: Handle the System.ArgumentNullException
            }

            return false;
        }

        #endregion
    }
}