using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulidingBlocks.CQRS
{
    public interface ICommandHandler<in TCommand, TRequest>:IRequestHandler<TCommand, TRequest> 
        where TCommand :ICommand<TRequest> 
        where TRequest : notnull

    {
    }

    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
       where TCommand : ICommand<Unit>

    {
    }
}
