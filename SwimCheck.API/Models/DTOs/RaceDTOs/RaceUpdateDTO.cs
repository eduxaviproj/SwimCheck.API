using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.DTOs.RaceDTOs
{
    public class RaceUpdateDTO
    {
        [Required, RegularExpression("^(?i)(Butterfly|Backstroke|Breaststroke|Freestyle|Medley)$", // (?i) makes it case insensitive
            ErrorMessage = "Type of stroke must be 'Butterfly', 'Backstroke', 'Breaststroke', 'Freestyle' or 'Medley'")]
        public string Stroke { get; set; } = string.Empty;


        [Required, Range(25, 1500, ErrorMessage = "Must be between 25m and 1500m")] public int DistanceMeters { get; set; }
    }
}
