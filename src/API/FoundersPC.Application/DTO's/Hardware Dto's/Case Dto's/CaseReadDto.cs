namespace FoundersPC.Application
{
	public class CaseReadDto
	{
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public string Title { get; set; }

		public string Type { get; set; }

		public string MaxMotherboardSize { get; set; }

		public string Material { get; set; }

		public string WindowMaterial { get; set; }

		public bool TransparentWindow { get; set; }

		public string Color { get; set; }

		public double? Weight { get; set; }

		public int? Height { get; set; }

		public int? Width { get; set; }

		public int? Depth { get; set; }
	}
}