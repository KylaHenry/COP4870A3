using COP4870.ECommerce.Services;
using COP4870.ECommerce.Models;

namespace COP4870.ECommerce.UI.MAUI
{
    public partial class CheckoutPage : ContentPage
    {
        private readonly ShoppingCartService _cartService;
        private const decimal TaxRate = 0.07m; // 7%

        public CheckoutPage(ShoppingCartService cartService)
        {
            InitializeComponent();
            _cartService = cartService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GenerateReceipt();
        }

        private void GenerateReceipt()
        {
            DateTimeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");

            ReceiptItemsLayout.Clear(); // clear old items

            var cartItems = _cartService.GetCartItems();
            decimal subtotal = 0;

            foreach (var item in cartItems)
            {
                var grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },  // Product
                        new ColumnDefinition { Width = GridLength.Auto }, // Qty
                        new ColumnDefinition { Width = GridLength.Auto }, // Price
                        new ColumnDefinition { Width = GridLength.Auto }  // Total
                    },
                    Padding = new Thickness(0, 5)
                };

                grid.Add(new Label { Text = item.Product.Name }, 0, 0);
                grid.Add(new Label { Text = $"x{item.Quantity}", HorizontalOptions = LayoutOptions.Center }, 1, 0);
                grid.Add(new Label { Text = $"@ ${item.Product.Price:F2}", HorizontalOptions = LayoutOptions.End }, 2, 0);
                grid.Add(new Label { Text = $"= ${item.LineTotal:F2}", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.End }, 3, 0);

                ReceiptItemsLayout.Add(grid);
                subtotal += item.LineTotal;
            }

            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax;

            SubtotalLabel.Text = $"Subtotal: ${subtotal:F2}";
            TaxLabel.Text = $"Tax (7%): ${tax:F2}";
            TotalLabel.Text = $"Total: ${total:F2}";

            _cartService.ClearCart(); // empty cart after showing receipt
        }

        private async void OnMainMenuClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }

        private async void OnContinueShoppingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//inventory");
        }
    }
}
