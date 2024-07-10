using Discount.Grpc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Threading;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(Cart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandValidator :AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x=>x.Cart.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository _repository, DiscountProtoService.DiscountProtoServiceClient discountProto) 
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: communicate with Discount.Grpc and calculate lastest prices of products
            await DeductDiscount(command.Cart, cancellationToken);
            // create command entity from command object
            string userName = await _repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(userName);
        }
        private async Task DeductDiscount(Cart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.ShoppingCartItem)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
                cart.ShoppingCart.TotalPrice += item.Price*item.Quantity;
            }
        }
    }
}
