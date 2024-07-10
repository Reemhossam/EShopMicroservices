namespace Basket.API.Data
{
    public class BasketRepository(AppDbContext _db) : IBasketRepository
    {
        public async Task<Cart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var ShoppingCart = _db.ShoppingCarts.FirstOrDefault(x => x.UserName == userName);
            var ShoppingCartItem = _db.ShoppingCartItems.Where(i => i.CartUserName == userName);
            var cart = new Cart()
            {
                ShoppingCart = ShoppingCart,
                ShoppingCartItem = ShoppingCartItem.ToList(),
            };
            if (cart.ShoppingCart == null)
            {
                throw new BasketNotFoundException(userName);
            }
            return cart;
        }

        public async Task<string> StoreBasket(Cart cart, CancellationToken cancellationToken = default)
        {
            ShoppingCart ShoppingCart = new ShoppingCart
            {
                UserName = cart.ShoppingCart.UserName,
                TotalPrice = cart.ShoppingCart.TotalPrice
            };
            if (await _db.ShoppingCarts.AsNoTracking().FirstOrDefaultAsync(e => e.UserName == ShoppingCart.UserName) == null)
            {
                _db.ShoppingCarts.Add(ShoppingCart);
                foreach (var item in cart.ShoppingCartItem)
                {
                    item.CartUserName = ShoppingCart.UserName;
                    _db.ShoppingCartItems.Add(item);
                }
            }
            else
            {
                // if ShoppingCart is not null
                //check if shoppingCartItems in ShoppingCart
                foreach (var item in cart.ShoppingCartItem)
                {
                    if (await _db.ShoppingCartItems.AsNoTracking().FirstOrDefaultAsync(e => e.ProductId == item.ProductId) == null)
                    {
                        item.CartUserName = ShoppingCart.UserName;
                        _db.ShoppingCartItems.Add(item);
                    }
                    else
                    {
                        item.Quantity += _db.ShoppingCartItems.AsNoTracking().FirstOrDefault(e => e.ProductId == item.ProductId).Quantity;
                        item.CartUserName = ShoppingCart.UserName;
                        item.Id = _db.ShoppingCartItems.AsNoTracking().FirstOrDefault(e => e.ProductId == item.ProductId).Id;
                        _db.ShoppingCartItems.Update(item);
                    }
                }
                _db.ShoppingCarts.Update(ShoppingCart);
            }

            await _db.SaveChangesAsync(cancellationToken);
            //update cache
            return ShoppingCart.UserName;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            ShoppingCart cart = _db.ShoppingCarts.FirstOrDefault(p => p.UserName == userName);
            IEnumerable<ShoppingCartItem> shoppingCartItems = _db.ShoppingCartItems.Where(u => u.CartUserName == userName);
            foreach (var item in shoppingCartItems)
            {
                _db.ShoppingCartItems.Remove(item);
            }
            if (cart == null)
            {
                //throw new ProductNotFoundException(command.Id);
            }
            _db.ShoppingCarts.Remove(cart);
            _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
