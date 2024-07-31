using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    // Client shouldn't be allowed to update the ID
    public class UpdateRegionRequestDto
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
