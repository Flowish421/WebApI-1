namespace WebApI_1.DTOs
{
    public class GetWorkplaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Weather { get; set; } 
    }
}
