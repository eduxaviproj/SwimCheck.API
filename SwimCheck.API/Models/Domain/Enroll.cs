using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.Domain
{
    public class Enroll
    {
        public Guid Id { get; set; }
        [Required] public Guid AthleteId { get; set; }
        [Required] public Guid RaceId { get; set; }
        public DateTime EnrollDate { get; set; } = DateTime.UtcNow;


        // Navigation properties
        public Athlete Athlete { get; set; } = null!;
        public Race Race { get; set; } = null!;
    }
}
