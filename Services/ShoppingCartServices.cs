using COP4870.ECommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace COP4870.ECommerce.Services
{
    public class ShoppingCartService
    {
        private readonly Dictionary<string, List<CartItem>> _carts = new();
        private string _currentCart = "Main Cart"; // default

        public ShoppingCartService()
        {
            _carts[_currentCart] = new List<CartItem>();
        }

        public IEnumerable<string> GetCartNames() => _carts.Keys;

        public string GetCurrentCartName() => _currentCart;

        public void SwitchCart(string cartName)
        {
            _currentCart = cartName;
            if (!_carts.ContainsKey(cartName))
                _carts[cartName] = new List<CartItem>();
        }

        public List<CartItem> GetCartItems() => _carts[_currentCart];

        public void AddToCart(Product product, int quantity)
        {
            var cart = _carts[_currentCart];
            var item = cart.FirstOrDefault(c => c.Product.Id == product.Id);
            if (item != null)
                item.Quantity += quantity;
            else
                cart.Add(new CartItem { Product = product, Quantity = quantity });
        }

        public void RemoveFromCart(int productId, int quantity)
        {
            var cart = _carts[_currentCart];
            var item = cart.FirstOrDefault(c => c.Product.Id == productId);
            if (item == null) return;

            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                cart.Remove(item);
        }

        public void RemoveAllFromCart(int productId)
        {
            _carts[_currentCart].RemoveAll(c => c.Product.Id == productId);
        }

        public int GetQuantityInCart(int productId)
        {
            return _carts[_currentCart].FirstOrDefault(c => c.Product.Id == productId)?.Quantity ?? 0;
        }

        public void ClearCart()
        {
            _carts[_currentCart].Clear();
        }
    }

}
