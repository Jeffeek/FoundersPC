using FoundersPC.Application.Base.Interfaces;
using FoundersPC.Domain.Common.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

namespace FoundersPC.Application
{
	public class SSDReadDto : ISSD, IIdentityItem, IProducerableDto
	{
        public double Factor { get; set; }
        public string Interface { get; set; }
        public int Volume { get; set; }
        public string MicroScheme { get; set; }
        public int SequentialRead { get; set; }
        public int SequentialRecording { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public ProducerReadDto Producer { get; set; }
    }
}