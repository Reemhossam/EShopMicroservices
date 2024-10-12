namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // create command entity from command object
            Order Order = CreateNewOrder(command.Order);

            //sava to database
            _dbContext.Orders.Add(Order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateOrderResult(Order.Id.Value);
        }
        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shoppingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress,orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var payment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            var order = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(orderDto.CustomerId), OrderName.Of(orderDto.OrderName),shoppingAddress, billingAddress, payment);

            foreach (var orderItem in orderDto.OrderItems)
                order.Add(ProductId.Of(orderItem.ProductId), orderItem.Quantity, orderItem.Price);
            return order;
        }
    }
    
}
