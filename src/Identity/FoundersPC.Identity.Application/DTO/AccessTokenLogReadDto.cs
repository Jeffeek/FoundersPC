using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoundersPC.Identity.Application.DTO
{
    public class AccessTokenLogReadDto
    {
        public int ApiAccessUsersTokenId { get; set; }

        public DateTime RequestDateTime { get; set; }
    }
}
