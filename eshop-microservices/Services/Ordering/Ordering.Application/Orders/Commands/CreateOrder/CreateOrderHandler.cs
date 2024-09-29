using BulidingBlocks.CQRS;
using Mapster;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // create command entity from command object
            Order Order = command.Order.Adapt<Order>();
            //sava to database

            //return result
            return new CreateOrderResult(Order.Id);
        }
    }
}
