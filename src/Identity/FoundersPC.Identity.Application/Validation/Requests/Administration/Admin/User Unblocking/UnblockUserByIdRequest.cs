using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Unblocking;

namespace FoundersPC.Identity.Application.Validation.Requests.Administration.Admin.User_Unblocking
{
	public class UnblockUserByIdRequestValidator : AbstractValidator<UnblockUserByIdRequest>
	{
		public UnblockUserByIdRequestValidator()
		{
			RuleFor(x => x.UserId)
				.GreaterThan(0);
		}
	}
}
