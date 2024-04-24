namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
    public record GetBasketResult(Cart Cart);
    public class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //TODO:get basket from database
            var cart = await _repository.GetBasket(query.UserName, cancellationToken);
            
            return new GetBasketResult(cart);
        }
    }
}
