using SwimCheck.API.Models.Domain.Enum;

namespace SwimCheck.API.Models.DTOs.RaceDTOs
{
    public class RaceViewDTO
    {
        public Guid Id { get; set; }
        public Stroke Stroke { get; set; } //ex: "Freestyle", "Backstroke", "Breaststroke", "Butterfly"
        public int DistanceMeters { get; set; } //ex: "50", "200" in meters
        public string DisplayName => $"{DistanceMeters}m {Stroke}"; //ex: "50m Freestyle"
    }
}
