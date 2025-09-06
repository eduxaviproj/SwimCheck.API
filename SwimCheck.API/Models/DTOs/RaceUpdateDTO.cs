namespace SwimCheck.API.Models.DTOs
{
    public class RaceUpdateDTO
    {
        public Guid Id { get; set; }
        public string Stroke { get; set; } = null!;
        public int DistanceMeters { get; set; }
        public string DisplayName { get; set; } = null!;
    }
}
