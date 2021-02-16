﻿#region Using derectives

using System;
using System.ComponentModel.DataAnnotations;
using FoundersPC.Application.Base.Interfaces;
using FoundersPC.Domain.Common.Interfaces.Hardware;

#endregion

namespace FoundersPC.Application
{
	public class SSDUpdateDto : ISSD, IProducerIdentiable
	{
        public double Factor { get; set; }
        public string Interface { get; set; }
        public int Volume { get; set; }
        public string MicroScheme { get; set; }
        public int SequentialRead { get; set; }
        public int SequentialRecording { get; set; }
        public string Title { get; set; }
        public int ProducerId { get; set; }
    }
}