namespace AgriConnect.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
