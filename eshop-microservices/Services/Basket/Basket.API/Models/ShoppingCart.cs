namespace Basket.API.Models
{
    public class ShoppingCart
    {
        [Key]
        public string UserName { get; set; } = default!;
        public decimal  TotalPrice { get; set; }
        public ShoppingCart(string userName, decimal totalPrice)
        {
            UserName= userName;
            TotalPrice= totalPrice;
        }
        //Required for Mapping
        public ShoppingCart()
        {
            
        }
    }
}
