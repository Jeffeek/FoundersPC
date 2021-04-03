#region Using namespaces

using System.Collections.Generic;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.Entrances
{
    public class EntrancesViewModel
    {
        public bool IsDatePickerRequired { get; set; } = true;

        public IEnumerable<UserEntranceLogReadDto> Entrances { get; set; }

        public EntrancesBetweenFilter BetweenFilter { get; set; }
    }
}