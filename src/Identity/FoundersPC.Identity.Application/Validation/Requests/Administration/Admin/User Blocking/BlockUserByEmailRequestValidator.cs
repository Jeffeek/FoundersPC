using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking;

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Blocking
{
    public class BlockUserByEmailRequestValidator : AbstractValidator<BlockUserByEmailRequest>
    {
        public BlockUserByEmailRequestValidator()
        {
            RuleFor(x => x.UserEmail)
                    .NotNull()
                    .NotEmpty()
                    .EmailAddress();
        }
    }
}
