#region Using namespaces

using FoundersPC.Domain.Common;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class HardwareType : IIdentityItem
{
    public string Name { get; set; } = default!;
    public int Id { get; set; }
}