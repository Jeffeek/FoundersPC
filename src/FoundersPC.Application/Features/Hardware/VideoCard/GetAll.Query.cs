using System;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.SharedKernel.Extensions;

namespace FoundersPC.Application.Features.Hardware.VideoCard;

public class GetAllQuery : GetAllHardwareQuery<Domain.Entities.Hardware.VideoCard>
{
    public bool? IsIntegrated { get; set; }

    public override Expression<Func<Domain.Entities.Hardware.VideoCard, bool>> GetExpression()
    {
        var result = base.GetExpression();

        if (IsIntegrated.HasValue)
            result = result.And(x => x.Metadata.IsIntegrated == IsIntegrated);

        return result;
    }
}