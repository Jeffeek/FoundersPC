using System;
using System.Linq.Expressions;
using FoundersPC.SharedKernel.Query;

namespace FoundersPC.Application.Features.Producer;

public class GetQuery : Query<Domain.Entities.Hardware.Producer>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.Hardware.Producer, bool>> GetExpression() =>
        item => item.Id == Id;
}