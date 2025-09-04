using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.DTOs
{
    public class EnrollCreateDTO
    {
        [Required]
        public Guid AthleteId { get; set; }


        [Required]
        public Guid RaceId { get; set; }
    }
}
