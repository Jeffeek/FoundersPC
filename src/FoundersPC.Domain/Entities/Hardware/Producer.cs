#region Using namespaces

using System;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Producer : FullAuditable, IIdentityItem
{
    public string? ShortName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int? CountryId { get; set; } = default!;
    public string? Website { get; set; } = default!;
    public DateTime? FoundationDate { get; set; } = default!;
    public Country? Country { get; set; } = default!;
    public int Id { get; set; }
}