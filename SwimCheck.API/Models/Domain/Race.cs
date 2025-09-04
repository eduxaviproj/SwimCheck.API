using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.Domain
{
    public class Race
    {
        public Guid Id { get; set; }

        [Required]
        public Stroke Stroke { get; set; } //ex: "Freestyle", "Backstroke", "Breaststroke", "Butterfly"

        [Required, Range(25, 1500, ErrorMessage = "Distance must be in the range of 25m and 1500m")]
        public int DistanceMeters { get; set; } //ex: "50", "100", "200" in meters

        //Navigation property to get all enrolls of this race
        public List<Enroll> Enrolls { get; set; } = new List<Enroll>();
    }
}
