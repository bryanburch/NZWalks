﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:{port}/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        // GET - ALL REGIONS
        // GET: https://localhost:{port}/api/regions
        [HttpGet]
        public IActionResult GetAll() 
        {
            // Get data from database (Domain Model)
            var regionsDomain = dbContext.Regions.ToList();

            // Map model to DTO
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // Exposing the DTO instead of the Domain Model
            return Ok(regionsDto);
        }

        // GET - SINGLE REGION (Get Region by ID)
        // GET: https://localhost:{port}/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] // tells EF to map url variable to action method parameter
        public IActionResult GetById([FromRoute] Guid id)
        {
            // The Find() method can only be used to search on ONLY the primary key
            //var region = dbContext.Regions.Find(id);

            // The FirstOrDefault() method can be used to search on ANY column attribute
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Exposing the DTO instead of the Domain Model
            return Ok(regionDto);
        }

        // POST - CREATE NEW REGION
        // POST: https://localhost:{port}/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // For a successful HTTP POST request, the server needs to return a "201 Created"
            // response. Along with the resource that was created in the reponse body
            // (we'll use that DTO version of the resource as the return value)
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // PUT - UPDATE REGION
        // PUT: https://localhost:{port}/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // First check if this region id even exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map changes to Domain Model based on the client DTO
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            
            dbContext.SaveChanges();

            // Convert the updated Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            
            return Ok(regionDto);
        }
    }
}
