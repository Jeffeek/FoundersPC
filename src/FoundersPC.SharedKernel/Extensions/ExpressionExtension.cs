using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoundersPC.SharedKernel.Extensions
{
    public static class ExpressionExtension
    {
        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left, Expression<TDelegate> right) =>
            Expression.Lambda<TDelegate>(Expression.AndAlso(left, right), left.Parameters);

        public static Expression<TDelegate> OrElse<TDelegate>(this Expression<TDelegate> left, Expression<TDelegate> right) =>
            Expression.Lambda<TDelegate>(Expression.OrElse(left, right), left.Parameters);

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new
                                                        {
                                                            f,
                                                            s = second.Parameters[i]
                                                        })
                           .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) =>
            first.Compose(second, Expression.And);

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second) =>
            first.Compose(second, Expression.Or);

        public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(this Expression<Func<T1, T2>> first, Expression<Func<T2, T3>> second)
        {
            var param = Expression.Parameter(typeof(T1), "param");
            var newFirst = new ReplaceVisitor(first.Parameters.First(), param).Visit(first.Body);
            var newSecond = new ReplaceVisitor(second.Parameters.First(), newFirst).Visit(second.Body);

            return Expression.Lambda<Func<T1, T3>>(newSecond, param);
        }
    }

    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map) =>
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp) =>
            new ParameterRebinder(map).Visit(exp);

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (_map.TryGetValue(p, out var replacement))
                p = replacement;

            return base.VisitParameter(p);
        }
    }

    public class ReplaceVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;

        public ReplaceVisitor(Expression from, Expression to)
        {
            _from = from;
            _to = to;
        }

        public override Expression Visit(Expression node) => node == _from ? _to : base.Visit(node);
    }
}