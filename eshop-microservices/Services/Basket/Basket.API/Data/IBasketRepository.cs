namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<Cart> GetBasket(string userName, CancellationToken cancellationToken = default);
        Task<string> StoreBasket(Cart cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}
