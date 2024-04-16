using Catalog.API.Data;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsResults>;
    public record GetProductsResults(IEnumerable<Product> Products);
    internal class CetProductsQueryHandler(AppDbContext _db,ILogger<CetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResults>
    {
        public async Task<GetProductsResults> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("CetProductsQueryHandler.Handle called with{@Query}", query);
            IEnumerable<Product> products = _db.Products.ToList();
            return new GetProductsResults(products);
        }
    }
}
