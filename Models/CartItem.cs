namespace COP4870.ECommerce.Models
{
    public class CartItem
    {
        public Product Product { get; set; } = new(); // prevent null reference
        public int Quantity { get; set; }
        public decimal LineTotal => Product.Price * Quantity;
    }
}

