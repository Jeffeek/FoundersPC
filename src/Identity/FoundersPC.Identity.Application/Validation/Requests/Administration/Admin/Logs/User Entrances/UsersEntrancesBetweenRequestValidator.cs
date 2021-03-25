#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Logs.Entrances;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Logs.User_Entrances
{
    public class UsersEntrancesBetweenRequestValidator : AbstractValidator<UsersEntrancesBetweenRequest>
    {
        public UsersEntrancesBetweenRequestValidator()
        {
            RuleFor(x => x.Finish)
                .NotNull()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(x => x.Start)
                .NotNull()
                .LessThan(x => x.Finish);
        }
    }
}