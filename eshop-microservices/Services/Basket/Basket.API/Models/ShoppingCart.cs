namespace Basket.API.Models
{
    public class ShoppingCart
    {
        [Key]
        public string UserName { get; set; } = default!;
        public decimal  TotalPrice { get; set; }
        public IList<ShoppingCartItem> Items { get; set; }
        //Required for Mapping
        public ShoppingCart()
        {
            
        }
    }
}
