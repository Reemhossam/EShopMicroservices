namespace Basket.API.Models
{
    public class Cart
    {
        public ShoppingCart ShoppingCart { get; set; }
        public IEnumerable<ShoppingCartItem>? ShoppingCartItem { get; set; }
    }
}
