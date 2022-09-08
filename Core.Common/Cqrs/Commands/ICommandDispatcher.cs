namespace Core.Common.Cqrs.Commands
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand<TResult>;
    }
}
