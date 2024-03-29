﻿#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoundersPC.API.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.API.Domain.Entities
{
    [Index(nameof(Id))]
    public class CaseEntity : HardwareEntityBase, IEquatable<CaseEntity>
    {
        [Column("WindowMaterial")]
        [MinLength(3)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string WindowMaterial { get; set; }

        [Column("Type")]
        [MinLength(3)]
        [MaxLength(40)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string Type { get; set; }

        [Column("MaxMotherboardSize")]
        [MinLength(3)]
        [MaxLength(20)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string MaxMotherboardSize { get; set; }

        [Column("Material")]
        [MinLength(3)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string Material { get; set; }

        [Column("TransparentWindow")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public bool TransparentWindow { get; set; }

        [Column("Color")]
        [MinLength(2)]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataType(DataType.Text)]
        [Required]
        public string Color { get; set; }

        [Column("Weight")]
        [Range(0.1, 100)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public double? Weight { get; set; }

        [Column("Height")]
        [Range(0.1, 1000)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Height { get; set; }

        [Column("Width")]
        [Range(0.1, 1000)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Width { get; set; }

        [Column("Depth")]
        [Range(0.1, 1000)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Depth { get; set; }

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(CaseEntity other)
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

            return obj.GetType() == GetType() && Equals((CaseEntity)obj);
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