using System.Collections.Generic;
using FoundersPC.API.Domain.Common;

namespace FoundersPC.API.Domain.Entities.Hardware.Additional
{
    public class Socket : IdentityItem
    {
        public string Title { get; set; } = default!;

        public ICollection<Motherboard>? Motherboards { get; set; } = default!;
    }
}