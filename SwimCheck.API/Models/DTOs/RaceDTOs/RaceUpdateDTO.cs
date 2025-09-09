using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.DTOs.RaceDTOs
{
    public class RaceUpdateDTO
    {
        [Required] public string Stroke { get; set; } = null!; // ex: Freestyle, Butterfly, etc
        [Required, Range(25, 1500)] public int DistanceMeters { get; set; }
    }
}
