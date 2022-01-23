using System;

namespace FoundersPC.Application.Features.Producer.Models;

public class ProducerInfo
{
    public int Id { get; set; }
    public string? ShortName { get; set; }
    public string FullName { get; set; } = default!;
    public string? Website { get; set; }
    public DateTime? FoundationDate { get; set; }
    public int? CountryId { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; } = default!;
}