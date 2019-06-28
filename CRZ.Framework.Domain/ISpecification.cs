using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRZ.Framework.Domain
{
    public interface ISpecification<T>
        where T : class, IAggregationRoot
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeStrings { get; }
    }
}
