using Catalog.API.Products.Update_Product;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) :ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidtors : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidtors()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }
    internal class DeleteProductCommandHandler(AppDbContext _db) : 
        ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            Product product = _db.Products.FirstOrDefault(p => p.Id == command.Id);
            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return new DeleteProductResult(true);
        }
    }
}
