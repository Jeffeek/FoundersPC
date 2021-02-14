#region Using derectives

using System;

#endregion

namespace FoundersPC.Application
{
	public class PowerSupplyReadDto
	{
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public DateTime? MarketLaunch { get; set; }

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