namespace AgriConnect.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // "farmer", "buyer", "officer"
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public int? CooperativeId { get; set; } // foreign key to the coop they belong to
        public Cooperative Cooperative { get; set; }


        // Navigation
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
