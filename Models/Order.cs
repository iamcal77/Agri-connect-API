namespace AgriConnect.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int? CooperativeId { get; set; }         // foreign key
        public Cooperative Cooperative { get; set; }    // navigation property

    }
}
