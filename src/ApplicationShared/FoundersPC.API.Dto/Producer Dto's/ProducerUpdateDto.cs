#region Using namespaces

using System;

#endregion

namespace FoundersPC.API.Dto
{
    public class ProducerUpdateDto
    {
        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Country { get; set; }

        public string Website { get; set; }

        public DateTime? FoundationDate { get; set; }
    }
}