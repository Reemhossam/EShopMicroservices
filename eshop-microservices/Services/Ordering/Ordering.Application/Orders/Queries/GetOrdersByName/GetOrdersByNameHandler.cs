using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrderByName
{
    public class GetOrdersByNameHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);
           
            //we will use Extension Method For Making Code More Maintainable.
            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
