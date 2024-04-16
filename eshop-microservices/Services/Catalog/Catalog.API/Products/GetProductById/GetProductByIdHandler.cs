

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(AppDbContext _db, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("CetProductByIdQueryHandler.Handle called with{@Query}", query);
            Product product = _db.Products.FirstOrDefault(p => p.Id == query.Id);  
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
