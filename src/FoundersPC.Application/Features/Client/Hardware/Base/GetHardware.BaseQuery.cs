using System;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetHardwareBaseQuery<THardware> : Query<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }

    public override Expression<Func<THardware, bool>> GetExpression() =>
        item => item.HardwareTypeId == HardwareTypeId && item.Id == Id;
}