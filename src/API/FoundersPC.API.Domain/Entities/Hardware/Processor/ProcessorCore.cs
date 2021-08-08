#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.API.Domain.Common;

#endregion

namespace FoundersPC.API.Domain.Entities.Hardware.Processor
{
    public class ProcessorCore : IdentityItem, IEquatable<ProcessorCore>
    {
        public DateTime? MarketLaunch { get; set; } = default!;

        public string? MicroArchitecture { get; set; } = default!;

        public int? L2CachePerCore { get; set; } = default!;

        public int? L3CachePerCore { get; set; } = default!;

        public string? Socket { get; set; } = default!;

        public string? Title { get; set; } = default!;

        public ICollection<Processor>? Processors { get; set; } = default!;

        #region Equality members

        /// <inheritdoc/>
        public bool Equals(ProcessorCore other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Nullable.Equals(MarketLaunch, other.MarketLaunch)
                   && Title == other.Title
                   && MicroArchitecture == other.MicroArchitecture
                   && L2CachePerCore == other.L2CachePerCore
                   && L3CachePerCore == other.L3CachePerCore
                   && Socket == other.Socket;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((ProcessorCore)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode() =>
            HashCode.Combine(MarketLaunch,
                             Title,
                             MicroArchitecture,
                             L2CachePerCore,
                             L3CachePerCore,
                             Socket);

        #endregion
    }
}