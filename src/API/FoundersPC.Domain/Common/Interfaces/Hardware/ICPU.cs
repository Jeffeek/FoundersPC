namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
	public interface ICPU
	{
		int TDP { get; set; }

		int MaxRamSpeed { get; set; }

		int Cores { get; set; }

		int Threads { get; set; }

		int Frequency { get; set; }

		int TurboBoostFrequency { get; set; }

		int TechProcess { get; set; }

		int L1Cache { get; set; }

		int L2Cache { get; set; }

		int L3Cache { get; set; }

		bool IntegratedGraphics { get; set; }

		string Series { get; set; }

		int ProcessorCoreId { get; set; }

		string Title { get; set; }
	}
}