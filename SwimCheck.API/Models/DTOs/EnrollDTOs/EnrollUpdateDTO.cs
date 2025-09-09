using SwimCheck.API.Models.DTOs.AthleteDTOs;
using SwimCheck.API.Models.DTOs.RaceDTOs;

namespace SwimCheck.API.Models.DTOs.EnrollDTOs
{
    public class EnrollUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Inclui info resumida do atleta e da prova
        public AthleteUpdateDTO Athlete { get; set; } = null!;
        public RaceUpdateDTO Race { get; set; } = null!;
    }
}
