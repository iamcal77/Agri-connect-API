namespace AgriConnect.Models
{
    public class ExtensionPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string ImageUrl { get; set; }
        public int PostedById { get; set; }
        public User PostedBy { get; set; }
    }
}
