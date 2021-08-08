#region Using namespaces

using System;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware
{
    [Index(nameof(Id))]
    public class Case : HardwareEntityBase, IEquatable<Case>
    {
        public string? WindowMaterial { get; set; } = default!;

        public string? Type { get; set; } = default!;

        public string? MaxMotherboardSize { get; set; } = default!;

        public string? Material { get; set; } = default!;

        public bool? TransparentWindow { get; set; } = default!;

        public string? Color { get; set; } = default!;

        public double? Weight { get; set; } = default!;

        public int? Height { get; set; } = default!;

        public int? Width { get; set; } = default!;

        public int? Depth { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(Case other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Type == other.Type
                   && MaxMotherboardSize == other.MaxMotherboardSize
                   && Material == other.Material
                   && WindowMaterial == other.WindowMaterial
                   && TransparentWindow == other.TransparentWindow
                   && Color == other.Color
                   && Depth == (other.Depth ?? 0)
                   && Width == (other.Width ?? 0)
                   && Height == (other.Height ?? 0)
                   && Weight.Equals(other.Weight ?? 0);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((Case)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(Type,
                             MaxMotherboardSize,
                             Material,
                             WindowMaterial,
                             TransparentWindow,
                             Color);

        #endregion
    }
}