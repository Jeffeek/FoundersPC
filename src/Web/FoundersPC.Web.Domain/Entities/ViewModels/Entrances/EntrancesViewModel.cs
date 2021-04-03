using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

namespace FoundersPC.Web.Domain.Entities.ViewModels.Entrances
{
    public class EntrancesViewModel
    {
        public bool IsDatePickerRequired { get; set; } = true;

        public IEnumerable<UserEntranceLogReadDto> Entrances { get; set; }

        public EntrancesBetweenFilter BetweenFilter { get; set; }
    }
}
