﻿using BulidingBlocks.CQRS;
using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.Order.Id).NotEmpty().WithMessage("Order id is required");
            RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        }
    }

}
