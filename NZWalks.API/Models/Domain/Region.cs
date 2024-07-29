namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        // RegionImageUrl is nullable
        public string? RegionImageUrl { get; set; }
    }
}
