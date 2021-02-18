namespace FoundersPC.Domain.Common.Interfaces.Hardware
{
	public interface IPowerSupply
	{
		int Power { get; set; }

		int? Efficiency { get; set; }

		string MotherboardPowering { get; set; }

		bool IsModular { get; set; }

		bool? CPU4PIN { get; set; }

		bool? CPU8PIN { get; set; }

		int FanDiameter { get; set; }

		bool Certificate80PLUS { get; set; }

		bool PFC { get; set; }

		string Title { get; set; }
	}
}