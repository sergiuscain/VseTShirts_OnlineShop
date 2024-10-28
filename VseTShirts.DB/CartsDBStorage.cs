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
        public Cart GetCartByUserId(string userId)
        {
            return _dbContext.Carts.Include(x=>x.Positions).ThenInclude(x=>x.Product).ThenInclude(p => p.Images).FirstOrDefault(c => c.UserId == userId);
        }

        public void Add(Guid productId, string userId)
        {

            var existingCart = GetCartByUserId(userId);
            var product = _dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Id == productId);
            if (existingCart == null)
            {
                var positions = new List<CartPosition> { new CartPosition{ Name = product.Name, Product = product, Quantity = 1} };
                var cart = new Cart {UserId = userId,Positions = positions };
                cart.Positions = positions;
                _dbContext.Carts.Add(cart);
            }
            else
            {
                var existingCartItem = existingCart?.Positions?.FirstOrDefault(p => p.Product.Id == product.Id);
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

        public void Remove(Guid productId, string userId)
        {
            var cart = GetCartByUserId(userId);
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
            var cart = GetCartByUserId(usertId);
            var removedCartPosition = cart.Positions.FirstOrDefault(p => p.Product.Id == productId);
            cart.Positions.Remove(removedCartPosition);
            if (cart.Positions.Count <= 0)
            {
                _dbContext.Carts.Remove(cart);
            }
            _dbContext.SaveChanges();
        }

        public void RemoveAll(string userId)
        {
            Cart removedCart = GetCartByUserId(userId);
            _dbContext.Carts.Remove(removedCart);
            _dbContext.SaveChanges();
        }

        public void Add(string productName, string userId)
        {

            var existingCart = GetCartByUserId(userId);
            var product = _dbContext.Products.Include(p => p.Images).FirstOrDefault(p => p.Name == productName);
            if (existingCart == null)
            {
                var positions = new List<CartPosition> { new CartPosition { Name = product.Name, Product = product, Quantity = 1 } };
                var cart = new Cart { UserId = userId, Positions = positions };
                cart.Positions = positions;
                _dbContext.Carts.Add(cart);
            }
            else
            {
                var existingCartItem = existingCart?.Positions?.FirstOrDefault(p => p.Product.Name == productName);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    existingCart.Positions.Add(new CartPosition { Name = product.Name, Product = product, Quantity = 1 });
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
