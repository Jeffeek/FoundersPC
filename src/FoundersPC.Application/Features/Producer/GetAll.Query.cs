using System;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Producer;

public class GetAllQuery : SortedQuery<Domain.Entities.Hardware.Producer>
{
    public string? SearchText { get; set; }
    public bool? ShowDeleted { get; set; }

    public override Expression<Func<Domain.Entities.Hardware.Producer, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (!String.IsNullOrEmpty(SearchText))
            SearchText.Split()
                      .ForEach(term =>
                               {
                                   result = result.And(x => x.FullName.Contains(term)
                                                            || x.ShortName != null && x.ShortName.Contains(term)
                                                            || x.Website != null && x.Website.Contains(term));
                               });

        if (ShowDeleted is false)
            result = result.And(x => !x.IsDeleted);

        return result;
    }
}