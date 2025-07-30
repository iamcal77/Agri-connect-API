namespace AgriConnect.Models
{
    public class Cooperative

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public int? LeaderId { get; set; }
        public User Leader { get; set; }
        public ICollection<User> Members { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
