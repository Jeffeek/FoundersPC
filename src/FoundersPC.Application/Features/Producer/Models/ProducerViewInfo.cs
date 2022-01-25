using System;

namespace FoundersPC.Application.Features.Producer.Models;

public class ProducerViewInfo
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public bool IsDeleted { get; set; }
}