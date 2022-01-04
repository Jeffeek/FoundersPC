using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetQuery : GetHardwareQuery<Domain.Entities.Hardware.PowerSupply>
{
    public override List<Expression<Func<Domain.Entities.Hardware.PowerSupply, object>>> GetIncludes()
    {
        var result = base.GetIncludes();

        result.Add(x => x.Metadata);

        return result;
    }
}