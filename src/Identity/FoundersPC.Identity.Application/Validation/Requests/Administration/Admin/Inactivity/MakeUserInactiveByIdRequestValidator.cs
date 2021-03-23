using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Inactivity;

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.Inactivity
{
	public class MakeUserInactiveByIdRequestValidator : AbstractValidator<MakeUserInactiveByIdRequest>
	{
		public MakeUserInactiveByIdRequestValidator()
		{
			RuleFor(x => x.UserId)
				.GreaterThan(0);
		}
	}
}
