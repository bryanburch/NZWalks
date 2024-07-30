using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:{port}/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET - ALL REGIONS
        // GET: https://localhost:{port}/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            // Get data from database (Domain Model)
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs (two approaches)

            // Approach 1 (verbose & error prone):
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Code = regionDomain.Code,
            //        Name = regionDomain.Name,
            //        RegionImageUrl = regionDomain.RegionImageUrl
            //    });
            //}

            // Approach 2 (cleaner thanks to Auto Mapper):
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            // Exposing the DTO instead of the Domain Model
            return Ok(regionsDto);
        }

        // GET - SINGLE REGION (Get Region by ID)
        // GET: https://localhost:{port}/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] // tells EF to map url variable to action method parameter
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // The Find() method can only be used to search on ONLY the primary key
            //var region = dbContext.Regions.Find(id);

            // The FirstOrDefault() method can be used to search on ANY column attribute
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Exposing the DTO instead of the Domain Model
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST - CREATE NEW REGION
        // POST: https://localhost:{port}/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to create Region. Returns the Model back so
            // we can later return it to the client
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            // For a successful HTTP POST request, the server needs to return a "201 Created"
            // response. Along with the resource that was created in the reponse body
            // (we'll use that DTO version of the resource as the return value)
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT - UPDATE REGION
        // PUT: https://localhost:{port}/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE - DELETE REGION
        // DELETE: https://localhost:{port}/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if this id exists
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // OPTIONAL: return deleted Region back to client
            // Map Model to DTO
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
