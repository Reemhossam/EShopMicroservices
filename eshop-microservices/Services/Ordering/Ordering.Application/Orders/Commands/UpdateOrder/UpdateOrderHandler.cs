using BulidingBlocks.CQRS;


namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDbContext _dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId= OrderId.Of(command.Order.Id);
            var order = await _dbContext.Orders.Include(o=>o.OrderItems).SingleOrDefaultAsync(o =>o.Id ==orderId,cancellationToken);
            if (order is null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }
            UpdateOrderWithNewValue(order, command.Order);
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateOrderResult(true);
        }
        private void UpdateOrderWithNewValue(Order order,OrderDto orderDto)
        {
            var updatedShoppingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var updatedBillingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);
            var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

            order.Update(OrderName.Of(orderDto.OrderName), updatedShoppingAddress, updatedBillingAddress, updatedPayment, orderDto.Status);

        }
    }
}
