namespace Basket.API.Data
{
    public class BasketRepository(AppDbContext _db) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var ShoppingCart = _db.ShoppingCarts.Include(i => i.Items).FirstOrDefault(x => x.UserName == userName);

            if (ShoppingCart == null)
            {
                throw new BasketNotFoundException(userName);
            }
            return ShoppingCart;
        }

        public async Task<string> StoreBasket(ShoppingCart ShoppingCart, CancellationToken cancellationToken = default)
        {
            if (await _db.ShoppingCarts.FirstOrDefaultAsync(e => e.UserName == ShoppingCart.UserName) == null)
            {
                _db.ShoppingCarts.Add(ShoppingCart);
                foreach (var item in ShoppingCart.Items)
                {
                    item.ShoppingCartUserName = ShoppingCart.UserName;
                    _db.ShoppingCartItems.Add(item);
                }
            }
            else
            {
                // if ShoppingCart is not null
                //check if shoppingCartItems in ShoppingCart
                foreach (var item in ShoppingCart.Items)
                {
                    if (await _db.ShoppingCartItems.FirstOrDefaultAsync(e => e.ProductId == item.ProductId) == null)
                    {
                        item.ShoppingCartUserName = ShoppingCart.UserName;
                        _db.ShoppingCartItems.Add(item);
                    }
                    else
                    {
                        item.Quantity += _db.ShoppingCartItems.FirstOrDefault(e => e.ProductId == item.ProductId).Quantity;
                        item.ShoppingCartUserName = ShoppingCart.UserName;
                        item.Id = _db.ShoppingCartItems.FirstOrDefault(e => e.ProductId == item.ProductId).Id;
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
            ShoppingCart ShoppingCart = _db.ShoppingCarts.FirstOrDefault(p => p.UserName == userName);
            IEnumerable<ShoppingCartItem> shoppingCartItems = _db.ShoppingCartItems.Where(u => u.ShoppingCartUserName == userName);
            foreach (var item in shoppingCartItems)
            {
                _db.ShoppingCartItems.Remove(item);
            }
            if (ShoppingCart == null)
            {
                //throw new ProductNotFoundException(command.Id);
            }
            _db.ShoppingCarts.Remove(ShoppingCart);
            _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
