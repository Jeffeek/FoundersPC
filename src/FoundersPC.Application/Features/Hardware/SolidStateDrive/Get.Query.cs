using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetQuery : GetHardwareQuery<Domain.Entities.Hardware.SolidStateDrive>
{
    public override List<Expression<Func<Domain.Entities.Hardware.SolidStateDrive, object>>> GetIncludes()
    {
        var result = base.GetIncludes();

        result.Add(x => x.Metadata);

        return result;
    }
}