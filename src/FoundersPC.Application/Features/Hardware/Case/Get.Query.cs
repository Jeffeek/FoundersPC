using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetQuery : GetHardwareQuery<Domain.Entities.Hardware.Case>
{
    public override List<Expression<Func<Domain.Entities.Hardware.Case, object>>> GetIncludes()
    {
        var result = base.GetIncludes();

        result.Add(x => x.Metadata);

        return result;
    }
}