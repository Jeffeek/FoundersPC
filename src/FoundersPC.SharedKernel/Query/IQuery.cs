using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FoundersPC.SharedKernel.Query
{
    public interface IQuery<TSource>
    {
        Expression<Func<TSource, bool>> GetExpression();

        List<Expression<Func<TSource, object>>> GetIncludes();
    }
}