#region Using namespaces

using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Domain.Common.Entrances
{
    public class EntrancesViewModel
    {
        public bool IsDatePickerRequired { get; set; } = true;

        public EntrancesBetweenFilter BetweenFilter { get; set; }

        public IndexViewModel<UserEntranceLogReadDto> IndexModel { get; set; }
    }
}