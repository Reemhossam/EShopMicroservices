namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default);
        Task<string> StoreBasket(ShoppingCart ShoppingCart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}
