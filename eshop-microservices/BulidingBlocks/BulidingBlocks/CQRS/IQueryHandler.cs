using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulidingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery, TRequest>:IRequestHandler<TQuery, TRequest> 
        where TQuery:IQuery<TRequest>
        where TRequest : notnull
    {
    }
}
