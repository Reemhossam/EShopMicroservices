
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize= 10):IQuery<GetProductsResults>;
    public record GetProductsResults(IEnumerable<Product> Products);
    internal class CetProductsQueryHandler(AppDbContext _db) : IQueryHandler<GetProductsQuery, GetProductsResults>
    {
        public async Task<GetProductsResults> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products =await _db.Products.ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
            return new GetProductsResults(products);

        }
    }
}
