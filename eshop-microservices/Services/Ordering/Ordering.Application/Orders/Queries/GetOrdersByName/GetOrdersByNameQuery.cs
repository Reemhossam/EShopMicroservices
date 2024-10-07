using BulidingBlocks.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public record GetOrdersByNameQuery(string name):IQuery<GetOrdersByNameResult>;
    
    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}
