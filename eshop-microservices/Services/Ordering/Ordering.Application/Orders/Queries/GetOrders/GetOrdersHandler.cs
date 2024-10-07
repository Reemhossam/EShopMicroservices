using BulidingBlocks.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext _dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.Request.PageIndex;
            var pageSize = query.Request.PageSize;
            var count = await _dbContext.Orders.LongCountAsync();
            var orders = await _dbContext.Orders
                            .Include(o => o.OrderItems)
                            .AsNoTracking()
                            .Skip(pageIndex * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancellationToken);
            return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, count, orders.ToOrderDtoList()));
        }
    }
}
