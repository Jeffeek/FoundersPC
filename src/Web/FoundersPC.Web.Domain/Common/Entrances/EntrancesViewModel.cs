#region Using namespaces

using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Web.Domain.Common.Entrances
{
    public class EntrancesViewModel
    {
        public bool IsDatePickerRequired { get; set; } = true;

        public bool IsPaginationRequired { get; set; } = true;

        public IndexViewModel<UserEntranceLogReadDto> IndexEntrances { get; set; }

        public EntrancesBetweenFilter BetweenFilter { get; set; }
    }
}