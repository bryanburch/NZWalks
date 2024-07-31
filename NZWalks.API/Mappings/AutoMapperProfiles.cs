using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            // Sets up a mapping from Region Domain Model (source) to
            // Region DTO (destination) and vice versa thanks to ReverseMap().
            // AutoMapper automatically handles lists of objects too.
            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<AddRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();

            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
        }
    }
}
