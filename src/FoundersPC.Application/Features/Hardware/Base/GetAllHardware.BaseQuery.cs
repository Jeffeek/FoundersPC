using System;
using System.Linq;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareQuery<THardware> : SortedQuery<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    public bool? IsDeleted { get; set; }

    public override Expression<Func<THardware, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (IsDeleted.HasValue)
            result = result.And(x => x.IsDeleted == IsDeleted);

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