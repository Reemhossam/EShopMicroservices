using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.API.Products.Update_Product
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler(AppDbContext _db, ILogger<UpdateProductCommand> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            // update command entity from command object
            logger.LogInformation("UpdateProductCommandHandler.Handle called with{@command}", command);
            Product product = _db.Products.FirstOrDefault(p => p.Id == command.Id);
            
            if (product == null)
            {
                throw new ProductNotFoundException();
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
