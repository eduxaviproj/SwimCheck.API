using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
