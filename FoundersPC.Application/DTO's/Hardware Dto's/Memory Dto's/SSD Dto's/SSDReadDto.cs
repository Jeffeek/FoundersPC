#region Using derectives

#endregion

namespace FoundersPC.Application
{
	public class SSDReadDto
	{
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public string Title { get; set; }

		public double Factor { get; set; }

		public string Interface { get; set; }

		public int Volume { get; set; }

		public string MicroScheme { get; set; }

		public int SequentialRead { get; set; }

		public int SequentialRecording { get; set; }
	}
}