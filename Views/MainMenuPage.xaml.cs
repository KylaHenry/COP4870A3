using Microsoft.Maui.Controls;
using COP4870.ECommerce.UI.MAUI;
namespace EcommerceApp.Views;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage()
    {
        InitializeComponent();
    }

    private async void OnInventoryClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("inventory");
    }

    private async void OnCartClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("cart");
    }
    private async void OnTaxSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TaxConfigPage));
    }

}
