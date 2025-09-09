using System.ComponentModel.DataAnnotations;

namespace SwimCheck.API.Models.Domain
{
    public class Athlete
    {
        public Guid Id { get; set; }
        [Required, MaxLength(120)] public string Name { get; set; } = null!;
        [Required] public DateTime BirthDate { get; set; }
        [MaxLength(80)] public string? Club { get; set; }

        //Navigation property to get all enrolls of this athlete and map EF Core relationship
        public List<Enroll> Enrolls { get; set; } = new List<Enroll>();
    }
}
