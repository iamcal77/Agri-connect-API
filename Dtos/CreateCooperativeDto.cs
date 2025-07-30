namespace AgriConnect.Dtos
{
    public class CreateCooperativeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int LeaderId { get; set; }
        public List<int>? MemberIds { get; set; } 
    }
}
