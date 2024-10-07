namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public class GetBasketQueryHandler(IBasketRepository _repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //TODO:get basket from database
            var ShoppingCart = await _repository.GetBasket(query.UserName, cancellationToken);
            
            return new GetBasketResult(ShoppingCart);
        }
    }
}
