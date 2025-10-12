using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Models.DTOs.AthleteDTOs;
using SwimCheck.API.Repositories.Interfaces;

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
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllAthletes([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var athletesModel = await athleteRepository.GetAllAthletesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            var athletesDTO = mapper.Map<List<AthleteViewDTO>>(athletesModel);

            return Ok(athletesDTO);
        }

        // GET: https://portnumber/api/athlete/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAthleteById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var athleteModel = await athleteRepository.GetAthleteByIdAsync(id);

            if (athleteModel == null)
            {
                return NotFound();
            }

            var athleteDTO = mapper.Map<AthleteViewDTO>(athleteModel);

            return Ok(athleteDTO);
        }

        // POST: https://portnumber/api/athlete
        [HttpPost]
        [Authorize(Roles = "Writer, Admin")]
        public async Task<IActionResult> CreateAthlete([FromBody] AthleteCreateDTO athleteCreateDTO)
        {
            //map DTO to Domain Model
            var athleteModel = mapper.Map<Models.Domain.Athlete>(athleteCreateDTO);

            //Use repository to create athlete
            var createdAthlete = await athleteRepository.CreateAthleteAsync(athleteModel);

            //Map created athlete back to DTO
            var athleteDTO = mapper.Map<AthleteViewDTO>(createdAthlete);

            return CreatedAtAction(nameof(GetAthleteById), new { id = athleteDTO.Id }, athleteDTO);
        }

        // PUT: https://portnumber/api/athlete/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Admin")]
        public async Task<IActionResult> UpdateAthlete([FromRoute] Guid id, [FromBody] AthleteUpdateDTO athleteUpdateDTO)
        {
            var athleteModel = mapper.Map<Athlete>(athleteUpdateDTO);
            athleteModel = await athleteRepository.UpdateAthleteAsync(id, athleteModel);

            if (athleteModel == null)
                return NotFound();

            var athleteDTO = mapper.Map<AthleteViewDTO>(athleteModel);
            return Ok(athleteDTO);
        }

        // DELETE: https://portnumber/api/athlete/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer, Admin")]
        public async Task<IActionResult> DeleteAthlete([FromRoute] Guid id)
        {
            var deletedAthlete = await athleteRepository.DeleteAthleteAsync(id);

            if (deletedAthlete == null)
                return NotFound();

            var athleteDTO = mapper.Map<AthleteViewDTO>(deletedAthlete);

            return Ok(athleteDTO);
        }
    }
}
