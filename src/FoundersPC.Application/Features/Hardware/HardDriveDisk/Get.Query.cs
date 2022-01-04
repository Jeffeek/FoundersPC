using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoundersPC.Application.Features.Hardware.Base;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class GetQuery : GetHardwareQuery<Domain.Entities.Hardware.HardDriveDisk>
{
    public override List<Expression<Func<Domain.Entities.Hardware.HardDriveDisk, object>>> GetIncludes()
    {
        var result = base.GetIncludes();

        result.Add(x => x.Metadata);

        return result;
    }
}