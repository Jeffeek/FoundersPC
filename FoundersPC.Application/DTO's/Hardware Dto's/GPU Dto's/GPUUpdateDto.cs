﻿using System;

namespace FoundersPC.Application
{
    public class GPUUpdateDto
    {
	    public int ProducerId { get; set; }

		public DateTime? MarketLaunch { get; set; }

		#region Memory

		public int VideoMemoryVolume { get; set; }

		public string VideoMemoryType { get; set; }

		public int VideoMemoryFrequency { get; set; }

		public int VideoMemoryBusWidth { get; set; }

		#endregion

		public int GraphicsProcessorId { get; set; }

		public int AdditionalPower { get; set; }

		#region Output

		public int VGA { get; set; }

		public int DVI { get; set; }

		public int HDMI { get; set; }

		public int DisplayPort { get; set; }

		#endregion
	}
}