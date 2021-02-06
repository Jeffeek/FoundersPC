#region Using derectives

#endregion

namespace FoundersPC.Application
{
	public class MotherboardReadDto
	{
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public string Title { get; set; }

		public string Socket { get; set; }

		public double Factor { get; set; }

		public string RAMSupport { get; set; }

		public int RAMSlots { get; set; }

		public string RAMMode { get; set; }

		public bool SLIOrCrossfire { get; set; }

		public string AudioSupport { get; set; }

		public bool WiFiSupport { get; set; }

		public bool PS2Support { get; set; }

		public int M2SlotsCount { get; set; }

		public string PCIExpressVersion { get; set; }
	}
}