#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.API.Application
{
	public class SSDUpdateDto : ISSD, IProducerIdentiable
	{
		public int ProducerId { get; set; }

		public double Factor { get; set; }

		public string Interface { get; set; }

		public int Volume { get; set; }

		public string MicroScheme { get; set; }

		public int SequentialRead { get; set; }

		public int SequentialRecording { get; set; }

		public string Title { get; set; }
	}
}