using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.API.Products.Update_Product
{
    public record UpdateProductCommand(Guid Id,string Name, string Category, string Description, string ImageFile, decimal Price):ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    
    public class UpdateProductCommandValidtors : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidtors()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is required")
                .Length(2,150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class UpdateProductCommandHandler(AppDbContext _db) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            // update command entity from command object
            Product product = _db.Products.FirstOrDefault(p => p.Id == command.Id);
            
            if (product == null)
            {
                throw new ProductNotFoundException(command.Id);
            }

             
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;
            
            _db.Products.Update(product);
            _db.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
