﻿#region Using namespaces

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.Entrances
{
    public class EntrancesBetweenFilter
    {
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime Finish { get; set; }
    }
}