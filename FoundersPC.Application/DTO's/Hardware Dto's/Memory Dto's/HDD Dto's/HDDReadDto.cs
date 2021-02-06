﻿namespace FoundersPC.Application
{
	public class HDDReadDto
	{
		public int Id { get; set; }

		public ProducerReadDto Producer { get; set; }

		public string Title { get; set; }

		public int HeadSpeed { get; set; }

		public int BufferSize { get; set; }

		public int Noise { get; set; }

		public double Factor { get; set; }

		public string Interface { get; set; }

		public int Volume { get; set; }
	}
}