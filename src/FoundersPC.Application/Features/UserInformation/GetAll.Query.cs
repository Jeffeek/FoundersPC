using System;
using System.Collections.Generic;
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
    public List<int>? RoleIds { get; set; }

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

        if (ShowBlocked is false)
            result = result.And(x => !x.IsBlocked);

        if (!RoleIds.IsEmpty())
            result = result.And(x => RoleIds!.Contains(x.RoleId));

        return result;
    }

    public override Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> GetSortingExpression() =>
        SortColumn switch
        {
            var id when String.Equals(id, "Id", StringComparison.OrdinalIgnoreCase) => item => item.ApplyOrder(x => x.Id, IsAscending),
            var login when String.Equals(login, "Login", StringComparison.OrdinalIgnoreCase) => item => item.ApplyOrder(x => x.Login, IsAscending),
            var email when String.Equals(email, "Email", StringComparison.OrdinalIgnoreCase) => item => item.ApplyOrder(x => x.Email, IsAscending),
            var role when String.Equals(role, "Role", StringComparison.OrdinalIgnoreCase) => item => item.ApplyOrder(x => x.ApplicationRole.Name, IsAscending),
            _ => item => item.ApplyOrder(x => x.Id, IsAscending),
        };
}