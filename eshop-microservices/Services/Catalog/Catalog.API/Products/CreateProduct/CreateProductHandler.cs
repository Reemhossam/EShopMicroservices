using Catalog.API.Data;
using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductRequestValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductCommandHandler(AppDbContext _db, IValidator<CreateProductCommand> validator) :
        ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command, cancellationToken);
            var errors = result.Errors.Select(e=>e.ErrorMessage).ToList();
            if(errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }
            // create command entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            //sava to database
            // session.Store(product);
            // await session.SaveChangesAsync(cancellationToken);

            _db.Products.Add(product);
            _db.SaveChanges();

            //return result
            return new CreateProductResult(product.Id);
        }
    }
}
