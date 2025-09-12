namespace SwimCheck.API.Models.DTOs.EnrollDTOs
{
    public class EnrollViewDTO
    {
        public Guid Id { get; set; }
        public Guid AthleteId { get; set; }
        public Guid RaceId { get; set; }
        public string AthleteName { get; set; } = null!;
        public string RaceDisplayName { get; set; } = null!;
    }
}
