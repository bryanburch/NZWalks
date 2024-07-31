using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    // Removed ID property since we're not asking the client to
    // generate the ID when creating a new Region (the API will
    // do that when inserting it into the database)
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of three characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of three characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
