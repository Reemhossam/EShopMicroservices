namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        [Key] public int Id { get; set; }
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public Guid ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string CartUserName { get; set; }
        [ForeignKey("CartUserName")]  //This attribute is optional bc EF should recognize Product obj specifi
        public virtual ShoppingCart ShoppCartUserName { get; set; }
    }
}
