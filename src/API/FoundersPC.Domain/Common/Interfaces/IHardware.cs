using FoundersPC.Domain.Entities.Hardware;

namespace FoundersPC.Domain.Common.Interfaces
{
    public interface IHardware : IIdentityItem
    {
        int ProducerId { get; set; }

        Producer Producer { get; set; }

        string Title { get; set; }
    }
}