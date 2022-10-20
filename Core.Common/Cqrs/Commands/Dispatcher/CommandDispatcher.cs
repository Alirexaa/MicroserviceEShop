using Microsoft.Extensions.DependencyInjection;
namespace Core.Common.Cqrs.Commands.Dispatcher;
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand<TResult>
    {
        using var scope = _serviceProvider.CreateScope();

        Task<TResult> handler() => scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>().HandleAsync(command, cancellationToken);


        var commandPipelines = scope.ServiceProvider.GetServices<ICommandPipelineBehavior<TCommand, TResult>>();
        var result = await commandPipelines.Aggregate((CommandHandlerDelegate<TResult>)handler, (next, pipeline)  => () => pipeline.HandelAsync(command,cancellationToken,next))();
        return result;
    }
}
