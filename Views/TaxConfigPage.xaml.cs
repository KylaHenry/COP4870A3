using Microsoft.Maui.Storage;

namespace COP4870.ECommerce.UI.MAUI
{
    public partial class TaxConfigPage : ContentPage
    {
        private const string TaxKey = "SalesTaxRate";

        public TaxConfigPage()
        {
            InitializeComponent();

            // Load and display current saved tax rate
            double savedRate = Preferences.Get(TaxKey, 0.07); // default to 7%
            TaxRateEntry.Text = (savedRate * 100).ToString("F2"); // display as percentage
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (double.TryParse(TaxRateEntry.Text, out double percent))
            {
                if (percent < 0 || percent > 100)
                {
                    await DisplayAlert("Invalid", "Tax rate must be between 0 and 100.", "OK");
                    return;
                }

                double decimalRate = percent / 100.0;
                Preferences.Set(TaxKey, decimalRate);

                StatusLabel.Text = $"Saved tax rate: {percent:F2}%";
                StatusLabel.TextColor = Colors.Green;
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid number (e.g. 6.5)", "OK");
                StatusLabel.Text = "Invalid number format";
                StatusLabel.TextColor = Colors.Red;
            }
        }

        public static double GetTaxRate()
        {
            return Preferences.Get(TaxKey, 0.07);
        }
        private async void OnMainMenuClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//main");
        }


    }
}
