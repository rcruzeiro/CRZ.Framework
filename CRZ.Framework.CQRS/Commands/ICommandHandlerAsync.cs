using System.Threading;
using System.Threading.Tasks;

namespace CRZ.Framework.CQRS.Commands
{
    public interface ICommandHandlerAsync<T>
        where T : class, ICommand
    {
        Task Execute(T command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandlerAsync<T, TResult>
        where T : class, ICommand
        where TResult : class
    {
        Task<TResult> ExecuteAsync(T command, CancellationToken cancellationToken = default);
    }
}
