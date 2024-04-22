using Catalog.API.Products.GetProductById;
using Marten;
using Marten.Linq.QueryHandlers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Catalog.API.Products.GetProductByCategory
{
    record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
    record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(AppDbContext _db) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = _db.Products.Where(p => 
                        p.Category==query.Category).ToList();

            return new GetProductByCategoryResult(products);
        }
    }
}
