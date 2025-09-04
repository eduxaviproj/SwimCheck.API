using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwimCheck.API.Repositories;

namespace SwimCheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IAthleteRepository athleteRepository;
        public AthleteController(IMapper mapper, IAthleteRepository athleteRepository)
        {
            this.mapper = mapper;
            this.athleteRepository = athleteRepository;
        }


        // GET: https://portnumber/api/athlete
        [HttpGet]
        public async Task<IActionResult> GetAllAthletes([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var athletes = await athleteRepository.GetAllAthletesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(athletes);
        }


    }
}
