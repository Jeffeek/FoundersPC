namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
	public interface ISSD
	{
		double Factor { get; set; }

		string Interface { get; set; }

		int Volume { get; set; }

		string MicroScheme { get; set; }

		int SequentialRead { get; set; }

		int SequentialRecording { get; set; }

		string Title { get; set; }
	}
}