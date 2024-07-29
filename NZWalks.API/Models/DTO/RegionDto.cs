namespace NZWalks.API.Models.DTO
{
    // Serves the purpose of decoupling the Region model from the database
    public class RegionDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
