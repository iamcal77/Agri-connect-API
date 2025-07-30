namespace AgriConnect.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string Status { get; set; }
        public string LogisticsProvider { get; set; }
    }
}
