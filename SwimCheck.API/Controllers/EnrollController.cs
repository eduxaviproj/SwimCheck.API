using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwimCheck.API.Models.DTOs.EnrollDTOs;
using SwimCheck.API.Repositories.Interfaces;

namespace SwimCheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEnrollRepository enrollRepository;

        public EnrollController(IMapper mapper, IEnrollRepository enrollRepository)
        {
            this.mapper = mapper;
            this.enrollRepository = enrollRepository;
        }

        // GET: https://portnumber/api/enroll
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollViewDTO>>> GetAllEnrolls([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
                                                                                    [FromQuery] string? sortBy, [FromQuery] bool isAscending = true,
                                                                                        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var enrolls = await enrollRepository.GetAllEnrollsAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            var enrollsDTO = mapper.Map<List<EnrollViewDTO>>(enrolls);

            return Ok(enrollsDTO);
        }

        // POST: https://portnumber/api/enroll
        [HttpPost]
        public async Task<IActionResult> CreateEnroll([FromBody] EnrollCreateDTO enrollCreateDTO)
        {
            var enrollModel = mapper.Map<Models.Domain.Enroll>(enrollCreateDTO);

            var createdEnroll = await enrollRepository.CreateEnrollAsync(enrollModel);

            if (createdEnroll is null)
                return BadRequest("Invalid AthleteId or RaceId, or enroll already exists.");

            var enrollDTO = mapper.Map<EnrollViewDTO>(createdEnroll);
            return CreatedAtAction(nameof(GetAllEnrolls), new { id = enrollDTO.Id }, enrollDTO);
        }

        // GET: https://portnumber/api/enroll/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEnrollById([FromRoute] Guid id)
        {
            var enrollModel = await enrollRepository.GetEnrollByIdAsync(id);

            if (enrollModel is null)
                return NotFound();

            var enrollDTO = mapper.Map<EnrollViewDTO>(enrollModel);
            return Ok(enrollDTO);
        }

        // DELETE: https://portnumber/api/enroll/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEnrollAsync([FromRoute] Guid id)
        {
            var existingEnroll = await enrollRepository.DeleteEnrollAsync(id);

            if (existingEnroll is null)
                return NotFound();

            var enrollDTO = mapper.Map<EnrollViewDTO>(existingEnroll);

            return Ok(enrollDTO);
        }

    }
}
