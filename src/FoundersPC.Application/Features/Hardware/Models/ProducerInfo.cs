using System;

namespace FoundersPC.Application.Features.Hardware.Models;

public class ProducerInfo
{
    public int Id { get; set; }
    public string? ShortName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? Website { get; set; } = default!;
    public DateTime? FoundationDate { get; set; } = default!;
    public int? CountryId { get; set; } = default!;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; } = default!;
}