

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(AppDbContext _db) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == query.Id);  
            if (product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
