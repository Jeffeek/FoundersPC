using System;

namespace FoundersPC.Application.Features.Client.Producer.Models;

public class ClientProducerInfo
{
    public int Id { get; set; }
    public string? ShortName { get; set; }
    public string FullName { get; set; } = default!;
    public string? Website { get; set; }
    public DateTime? FoundationDate { get; set; }
    public string? Country { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}