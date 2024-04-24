﻿
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);
    public class DeleteBasketCommandValidtor:AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidtor()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class DeleteBasketCommandHandler(IBasketRepository _repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            bool response = await _repository.DeleteBasket(command.UserName, cancellationToken);
            return new DeleteBasketResult(response);
        }
    }
}
