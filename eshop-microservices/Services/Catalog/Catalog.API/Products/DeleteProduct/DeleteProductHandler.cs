namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductRequest(Guid Id) :ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandHandler(AppDbContext _db, ILogger<DeleteProductRequest> logger) : 
        ICommandHandler<DeleteProductRequest, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductRequest command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with{@command}", command);
            Product product = _db.Products.FirstOrDefault(p => p.Id == command.Id);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return new DeleteProductResult(true);
        }
    }
}
