using System;
using System.Linq;
using System.Linq.Expressions;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.UserInformation;

public class GetAllQuery : SortedQuery<ApplicationUser>
{
    public bool? ShowBlocked { get; set; }
    public string? SearchText { get; set; }

    public override Expression<Func<ApplicationUser, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (!String.IsNullOrEmpty(SearchText))
            SearchText.Split()
                      .ForEach(term =>
                               {
                                   result = result.And(x => x.Login.Contains(term)
                                                            || x.Email.Contains(term)
                                                            || x.ApplicationRole.Name.Contains(term)
                                                            || x.Tokens.Any(z => z.Token.Contains(term)));
                               });

        if (ShowBlocked.HasValue)
            result = result.And(x => x.IsBlocked == ShowBlocked);

        return result;
    }
}