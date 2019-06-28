namespace CRZ.Framework.CQRS.Commands
{
    public interface ICommandHandler<T>
        where T : class, ICommand
    {
        void Execute(T command);
    }

    public interface ICommandHandler<T, TResult>
        where T : class, ICommand
        where TResult : class
    {
        TResult Execute(T command);
    }
}
