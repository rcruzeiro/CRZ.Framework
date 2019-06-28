using System.Threading;
using System.Threading.Tasks;

namespace CRZ.Framework.CQRS.Queries
{
    public interface IQueryHandlerAsync<T, TResult>
        where T : class, IFilter
        where TResult : class
    {
        Task<TResult> HandleAsync(T filter, CancellationToken cancellationToken = default);
    }
}
