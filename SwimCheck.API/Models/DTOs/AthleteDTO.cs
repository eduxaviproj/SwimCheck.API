namespace SwimCheck.API.Models.DTOs
{
    public class AthleteDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public string? Club { get; set; }
    }
}
