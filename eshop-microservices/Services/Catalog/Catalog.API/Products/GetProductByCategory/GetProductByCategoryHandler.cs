using Catalog.API.Products.GetProductById;
using Marten;
using Marten.Linq.QueryHandlers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Catalog.API.Products.GetProductByCategory
{
    record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
    record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(AppDbContext _db, ILogger<GetProductByCategoryQuery> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with{@Query}", query);
            IEnumerable<Product> products = await _db.Products.Where(p => 
                        p.Category.Contains(query.Category)).ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
