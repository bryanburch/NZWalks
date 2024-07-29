namespace NZWalks.API.Models.DTO
{
    // Client shouldn't be allowed to update the ID
    public class UpdateRegionRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
