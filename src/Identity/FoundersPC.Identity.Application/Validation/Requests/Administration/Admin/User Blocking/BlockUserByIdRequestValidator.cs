using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking;

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Blocking
{
    public class BlockUserByIdRequestValidator : AbstractValidator<BlockUserByIdRequest>
    {
        public BlockUserByIdRequestValidator()
        {
            RuleFor(x => x.UserId)
                    .GreaterThan(0);
        }
    }
}
