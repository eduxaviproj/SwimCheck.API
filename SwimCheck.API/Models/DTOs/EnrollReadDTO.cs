namespace SwimCheck.API.Models.DTOs
{
    public class EnrollReadDTO
    {
        public Guid Id { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Inclui info resumida do atleta e da prova
        public AthleteReadDTO Athlete { get; set; } = null!;
        public RaceReadDTO Race { get; set; } = null!;
    }
}
