using COP4870.ECommerce.Models;
using COP4870.ECommerce.Services;
using System.Linq;

namespace COP4870.ECommerce.UI.MAUI
{
    [QueryProperty(nameof(RefreshRequired), "refresh")]
    public partial class InventoryPage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly ShoppingCartService _cartService;
        private bool _refreshRequired;

        private readonly Dictionary<int, int> _quantityMap = new();

        public string RefreshRequired
        {
            set
            {
                _refreshRequired = !string.IsNullOrEmpty(value) && Convert.ToBoolean(value);
                if (_refreshRequired)
                {
                    LoadProducts();
                }
            }
        }

        public InventoryPage(ProductService productService, ShoppingCartService cartService)
        {
            InitializeComponent();
            _productService = productService;
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProducts();

            // Show active cart (optional)
            CurrentCartLabel.Text = $"Active Cart: {_cartService.GetCurrentCartName()}";
        }


        private void LoadProducts()
        {
            ProductsListView.ItemsSource = _productService.ListProducts();
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product product)
            {
                ((ListView)sender).SelectedItem = null;
                var navigationParameter = new Dictionary<string, object>
                {
                    { "productId", product.Id.ToString() }
                };
                await Shell.Current.GoToAsync("productdetail", navigationParameter);
            }
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("productdetail");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }

        private void OnQuantityEntryChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry && entry.BindingContext is Product product)
            {
                if (int.TryParse(entry.Text, out int qty) && qty > 0)
                {
                    _quantityMap[product.Id] = qty;
                }
                else
                {
                    _quantityMap.Remove(product.Id);
                }
            }
        }

        private async void OnAddQuantityClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int productId)
            {
                var product = _productService.GetProduct(productId);
                if (product == null)
                {
                    await DisplayAlert("Error", "Product not found", "OK");
                    return;
                }

                int quantity = _quantityMap.ContainsKey(productId) ? _quantityMap[productId] : 1;

                if (quantity <= 0 || quantity > product.Quantity)
                {
                    await DisplayAlert("Error", "Invalid quantity or insufficient stock", "OK");
                    return;
                }

                _cartService.AddToCart(product, quantity);
                product.Quantity -= quantity;
                _productService.UpdateProduct(product);

                LoadProducts();
                StatusLabel.Text = $"Added {quantity} x {product.Name} to cart";
                await Task.Delay(3000);
                StatusLabel.Text = "";
            }
        }

        private void OnSortChanged(object sender, EventArgs e)
        {
            string option = SortPicker.SelectedItem?.ToString() ?? "Name";
            var products = _productService.ListProducts();

            if (option == "Name")
                ProductsListView.ItemsSource = products.OrderBy(p => p.Name).ToList();
            else if (option == "Price")
                ProductsListView.ItemsSource = products.OrderBy(p => p.Price).ToList();
        }
    }
}
