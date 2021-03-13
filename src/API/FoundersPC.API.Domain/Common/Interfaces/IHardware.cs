#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.API.Domain.Common.Interfaces
{
    public interface IHardware : IIdentityItem
    {
        int ProducerId { get; set; }

        Producer Producer { get; set; }

        string Title { get; set; }
    }
}