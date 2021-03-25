#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Entrances;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Logs.User_Entrances
{
    public class UsersEntrancesInRequestValidator : AbstractValidator<UsersEntrancesInRequest>
    {
        public UsersEntrancesInRequestValidator()
        {
            RuleFor(x => x.RequestDate)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}