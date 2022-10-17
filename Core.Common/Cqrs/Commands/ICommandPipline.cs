using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Cqrs.Commands
{
    public delegate Task<TCommandResult> CommandHandlerDelegate<TCommandResult>();
    public interface ICommandPipelineBehavior<TCommand,TCommandResult> 
    {
        public Task HandelAsync(TCommand command,CancellationToken cancellationToken,CommandHandlerDelegate<TCommandResult> next);
    }
}
