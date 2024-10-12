using BulidingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) :ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketCommandValidator:AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(b => b.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null");
            RuleFor(b => b.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName Is Required"); ;
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository _repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //get existing basket with total price
            //set total price in basketCheckout event message
            //send basketCheckout event to rabbitMQ using massTransit
            //DeleteBasket the basket

            var basket = await _repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null) 
            {
                return new CheckoutBasketResult(false);
            }
            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;
            //كده استخدمنا ال masstransit عشان نعمل publish للevent وبعتنا الداتا بتاعت الevent
            await publishEndpoint.Publish(eventMessage,cancellationToken);
            await _repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            return new CheckoutBasketResult(true);
        }
    }
}
