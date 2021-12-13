#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace FoundersPC.SharedKernel.Query;

public abstract class Query<TSource> : IQuery<TSource>
{
    public virtual Expression<Func<TSource, bool>> GetExpression() => uniqueEntity => true;

    public virtual List<Expression<Func<TSource, object>>> GetIncludes() => new();
}