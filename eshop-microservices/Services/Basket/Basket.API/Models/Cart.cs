namespace Basket.API.Models
{
    public class Cart
    {
        public ShoppingCart ShoppingCart { get; set; }
        public ICollection<ShoppingCartItem>? ShoppingCartItem { get; set; }
    }
}
