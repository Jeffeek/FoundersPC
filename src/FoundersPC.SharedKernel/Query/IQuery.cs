#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace FoundersPC.SharedKernel.Query;

public interface IQuery<TSource>
{
    Expression<Func<TSource, bool>> GetExpression();

    List<Expression<Func<TSource, object>>> GetIncludes();
}