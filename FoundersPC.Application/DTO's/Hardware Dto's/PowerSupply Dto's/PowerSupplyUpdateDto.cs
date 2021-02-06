#region Using derectives

#endregion

namespace FoundersPC.Application
{
	public class PowerSupplyUpdateDto
	{
		public int ProducerId { get; set; }

		public string Title { get; set; }

		public int Power { get; set; }

		public int Efficiency { get; set; }

		public string MotherboardPowering { get; set; }

		public bool IsModular { get; set; }

		public bool CPU4PIN { get; set; }

		public bool CPU8PIN { get; set; }

		public int FanDiameter { get; set; }

		public bool Certificate80PLUS { get; set; }

		public bool PFC { get; set; }
	}
}