using System;
using System.Linq;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareQuery<THardware> : SortedQuery<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    public bool? ShowDeleted { get; set; }
    public string? SearchText { get; set; }

    public override Expression<Func<THardware, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (!String.IsNullOrEmpty(SearchText))
            SearchText.Split()
                      .ForEach(term =>
                               {
                                   result = result.And(x => x.BaseMetadata.Title.Contains(term)
                                                            || x.BaseMetadata.Producer.FullName.Contains(term)
                                                            || x.BaseMetadata.Producer.ShortName != null && x.BaseMetadata.Producer.ShortName.Contains(term)
                                                            || x.BaseMetadata.HardwareType.Name.Contains(term));
                               });

        if (ShowDeleted is false)
            result = result.And(x => !x.IsDeleted);

        return result;
    }

    public override Func<IQueryable<THardware>, IOrderedQueryable<THardware>> GetSortingExpression() =>
        SortColumn switch
        {
            var producer when String.Equals(producer, "Producer", StringComparison.OrdinalIgnoreCase) =>
                item => item.ApplyOrder(x => x.BaseMetadata.Producer.FullName, IsAscending),
            _ => item => item.ApplyOrder(SortColumn, IsAscending)
        };
}