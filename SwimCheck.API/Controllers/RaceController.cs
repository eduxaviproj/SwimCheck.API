using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Models.DTOs.RaceDTOs;
using SwimCheck.API.Repositories.Interfaces;

namespace SwimCheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRaceRepository raceRepository;
        public RaceController(IMapper mapper, IRaceRepository raceRepository)
        {
            this.mapper = mapper;
            this.raceRepository = raceRepository;
        }

        // GET: https://portnumber/api
        [HttpGet]
        public async Task<IActionResult> GetAllRaces([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var racesModel = await raceRepository.GetAllRacesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var racesDTO = mapper.Map<List<RaceViewDTO>>(racesModel);

            return Ok(racesDTO);
        }

        // GET: https://portnumber/api
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRaceById([FromRoute] Guid id)
        {
            var raceModel = await raceRepository.GetRaceByIdAsync(id);

            if (raceModel == null)
            { return NotFound(); }

            var raceDTO = mapper.Map<RaceViewDTO>(raceModel);

            return Ok(raceDTO);
        }

        // POST: https://portnumber/api
        [HttpPost]
        public async Task<IActionResult> CreateRace([FromBody] RaceCreateDTO raceCreateDTO)
        {
            var raceModel = mapper.Map<Race>(raceCreateDTO); // map DTO to Domain model

            await raceRepository.CreateRaceAsync(raceModel); // save to DB

            var raceDTO = mapper.Map<RaceViewDTO>(raceModel); // map Domain model back to DTO

            return CreatedAtAction(nameof(GetRaceById), new { id = raceDTO.Id }, raceDTO); // return 201 status code with location header, and then the body of the created object
        }

        // PUT: https://portnumber/api
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRace([FromRoute] Guid id, [FromBody] RaceUpdateDTO raceUpdateDTO)
        {
            var raceModel = mapper.Map<Race>(raceUpdateDTO);

            var updatedRaceModel = await raceRepository.UpdateRaceAsync(id, raceModel);

            if (updatedRaceModel == null)
            { return NotFound(); }

            var raceDTO = mapper.Map<RaceViewDTO>(updatedRaceModel);

            return Ok(raceDTO);
        }

        // DELETE: https://portnumber/api
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRace([FromRoute] Guid id)
        {
            var raceModel = await raceRepository.DeleteRaceAsync(id);

            if (raceModel == null)
            { return NotFound(); }

            var raceDTO = mapper.Map<RaceViewDTO>(raceModel);
            return Ok(raceDTO);
        }

    }
}