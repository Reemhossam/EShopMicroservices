using Discount.Grpc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;
using Basket.API.Models;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart ShoppingCart) :ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator :AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x=>x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: communicate with Discount.Grpc and calculate lastest prices of products
            await DeductDiscount(command.ShoppingCart, cancellationToken);
            // create command entity from command object
            string userName = await _repository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(userName);
        }
        private async Task DeductDiscount(ShoppingCart ShoppingCart, CancellationToken cancellationToken)
        {
            foreach (var item in ShoppingCart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
                ShoppingCart.TotalPrice += item.Price*item.Quantity;
            }
        }
    }
}
