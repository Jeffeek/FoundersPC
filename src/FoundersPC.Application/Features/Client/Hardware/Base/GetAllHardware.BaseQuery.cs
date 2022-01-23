using System;
using System.Linq;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetAllHardwareQuery<THardware> : SortedQuery<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    public override Func<IQueryable<THardware>, IOrderedQueryable<THardware>> GetSortingExpression() =>
        SortColumn switch
        {
            var producer when String.Equals(producer, "Producer", StringComparison.OrdinalIgnoreCase) =>
                item => item.ApplyOrder(x => x.BaseMetadata.Producer.FullName, IsAscending),
            _ => item => item.ApplyOrder(SortColumn, IsAscending)
        };
}