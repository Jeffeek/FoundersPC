using System;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetHardwareQuery<THardware> : Query<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }

    public override Expression<Func<THardware, bool>> GetExpression() =>
        item => item.Id == Id && item.HardwareTypeId == HardwareTypeId;
}