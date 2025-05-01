using COP4870.ECommerce.Models;
using COP4870.ECommerce.Services;
using System.Linq;

namespace COP4870.ECommerce.UI.MAUI
{
    public partial class ShoppingCartPage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly ShoppingCartService _cartService;

        public ShoppingCartPage(ProductService productService, ShoppingCartService cartService)
        {
            InitializeComponent();
            _productService = productService;
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load cart names
            CartPicker.ItemsSource = _cartService.GetCartNames().ToList();

            // Set selected cart in Picker
            var current = _cartService.GetCurrentCartName();
            CartPicker.SelectedItem = current;

            RefreshCart();
        }


        private void RefreshCart()
        {
            var cartItems = _cartService.GetCartItems() ?? new List<CartItem>();
            string sortOption = SortPicker.SelectedItem?.ToString();

            if (sortOption == "Price")
                CartListView.ItemsSource = cartItems.OrderBy(c => c.Product.Price).ToList();
            else if (sortOption == "Name")
                CartListView.ItemsSource = cartItems.OrderBy(c => c.Product.Name).ToList();
            else
                CartListView.ItemsSource = cartItems;

            // Get user-configured tax rate
            double taxRate = TaxConfigPage.GetTaxRate();

            // Calculate totals
            decimal subtotal = cartItems.Sum(item => item.LineTotal);
            decimal tax = subtotal * (decimal)taxRate;
            decimal total = subtotal + tax;

            string formattedRate = (taxRate * 100).ToString("F2");

            SubtotalLabel.Text = $"Subtotal: ${subtotal:F2}";
            TaxLabel.Text = $"Tax ({formattedRate}%): ${tax:F2}";
            TotalLabel.Text = $"Total: ${total:F2}";
        }

        private void OnSortChanged(object sender, EventArgs e)
        {
            RefreshCart(); // Sorting is handled within RefreshCart
        }
        private void OnCartChanged(object sender, EventArgs e)
        {
            if (CartPicker.SelectedItem is string cartName)
            {
                _cartService.SwitchCart(cartName);
                RefreshCart();
            }
        }

        private async void OnNewCartClicked(object sender, EventArgs e)
        {
            string cartName = await DisplayPromptAsync("New Cart", "Enter a name for the new cart:");
            if (!string.IsNullOrWhiteSpace(cartName))
            {
                _cartService.SwitchCart(cartName);
                CartPicker.ItemsSource = _cartService.GetCartNames().ToList();
                CartPicker.SelectedItem = cartName;
                RefreshCart();
            }
        }

        private void OnIncreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null) return;

                if (product.Quantity <= 0)
                {
                    DisplayAlert("Error", "No more inventory available for this product", "OK");
                    return;
                }

                _cartService.AddToCart(product, 1);
                product.Quantity--;
                _productService.UpdateProduct(product);
                RefreshCart();
            }
        }

        private void OnDecreaseQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null) return;

                _cartService.RemoveFromCart(productId, 1);
                product.Quantity++;
                _productService.UpdateProduct(product);
                RefreshCart();
            }
        }

        private void OnRemoveFromCartClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                int quantityInCart = _cartService.GetQuantityInCart(productId);
                _cartService.RemoveAllFromCart(productId);

                var product = _productService.GetProduct(productId);
                if (product != null)
                {
                    product.Quantity += quantityInCart;
                    _productService.UpdateProduct(product);
                }

                RefreshCart();
            }
        }

        private async void OnContinueShoppingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//inventory");
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            if (_cartService.GetCartItems().Count == 0)
            {
                await DisplayAlert("Error", "Your cart is empty", "OK");
                return;
            }

            await Shell.Current.GoToAsync("//checkout");
        }
    }
}
