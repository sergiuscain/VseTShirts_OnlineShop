using Microsoft.EntityFrameworkCore;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class CartsDBStorage : ICartsStorage
    {
        private readonly DatabaseContext _dbContext;

        public CartsDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _dbContext.Carts.Include(x=>x.Positions).ThenInclude(x=>x.Product).ThenInclude(p => p.Images).FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddAsync(Guid productId, string userId)
        {

            var existingCart = GetCartByUserIdAsync(userId);
            var product = await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);
            if (existingCart == null)
            {
                var positions = new List<CartPosition> { new CartPosition{ Name = product.Name, Product = product, Quantity = 1} };
                var cart = new Cart {UserId = userId,Positions = positions };
                cart.Positions = positions;
                await _dbContext.Carts.AddAsync(cart);
            }
            else
            {
                var existingCartItem = await existingCart?.Positions?.FirstOrDefaultAsync(p => p.Product.Id == product.Id);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    existingCart.Positions.Add(new CartPosition{ Name = product.Name, Product = product, Quantity = 1 });
                }
            }
                _dbContext.SaveChanges();
        }

        public void RemoveAsync(Guid productId, string userId)
        {
            var cart = GetCartByUserIdAsync(userId);
            var cartItem = cart.Positions.FirstOrDefault(p => p.Product.Id == productId);
            cartItem.Quantity--;
            if (cartItem.Quantity <= 0)
            {
                cart.Positions.Remove(cartItem);
                _dbContext.CartPositions.Remove(cartItem);
            }
            if (cart.Positions.Count <= 0)
            {
                _dbContext.Carts.Remove(cart);
            }
            _dbContext.SaveChanges();
        }
        public void RemovePosition(Guid productId, string usertId)
        {
            var cart = GetCartByUserIdAsync(usertId);
            var removedCartPosition = cart.Positions.FirstOrDefault(p => p.Product.Id == productId);
            cart.Positions.Remove(removedCartPosition);
            if (cart.Positions.Count <= 0)
            {
                _dbContext.Carts.Remove(cart);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveAllAsync(string userId)
        {
            Cart removedCart = GetCartByUserIdAsync(userId);
            _dbContext.Carts.Remove(removedCart);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(string productName, string userId)
        {

            var existingCart = await GetCartByUserIdAsync(userId);
            var product = await _dbContext.Products.Include(p => p.Images).FirstOrDefaultAsync(p => p.Name == productName);
            if (existingCart == null)
            {
                var positions = new List<CartPosition> { new CartPosition { Name = product.Name, Product = product, Quantity = 1 } };
                var cart = new Cart { UserId = userId, Positions = positions };
                cart.Positions = positions;
                _dbContext.Carts.Add(cart);
            }
            else
            {
                var existingCartItem = await existingCart?.Positions?.FirstOrDefaultAsync(p => p.Product.Name == productName);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    existingCart.Positions.Add(new CartPosition { Name = product.Name, Product = product, Quantity = 1 });
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
