#region Using namespaces

using FoundersPC.API.Application.Base.Interfaces;
using FoundersPC.API.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.API.Application
{
	public class HDDUpdateDto : IHDD, IProducerIdentiable
	{
		public int HeadSpeed { get; set; }

		public int BufferSize { get; set; }

		public int Noise { get; set; }

		public double Factor { get; set; }

		public string Interface { get; set; }

		public int Volume { get; set; }

		public string Title { get; set; }

		public int ProducerId { get; set; }
	}
}