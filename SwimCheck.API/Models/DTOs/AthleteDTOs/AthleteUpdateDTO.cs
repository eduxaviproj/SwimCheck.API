using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.DTOs.AthleteDTOs
{
    public class AthleteUpdateDTO
    {
        [Required, MaxLength(120)] public string Name { get; set; } = null!;
        [Required] public DateTime BirthDate { get; set; }
        [MaxLength(80)] public string? Club { get; set; }
    }
}
