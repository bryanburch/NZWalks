namespace NZWalks.API.Models.DTO
{
    // Removed ID property since we're not asking the client to
    // generate the ID when creating a new Region (the API will
    // do that when inserting it into the database)
    public class AddRegionRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
