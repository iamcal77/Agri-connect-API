namespace AgriConnect.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }

        public int FarmerId { get; set; }
        public User Farmer { get; set; }
    }
}
