using SwimCheck.API.Models.Domain;

namespace SwimCheck.API.Repositories.Interfaces
{
    public interface IAthleteRepository
    {
        public Task<List<Athlete>> GetAllAthletesAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        public Task<Athlete?> GetAthleteByIdAsync(Guid id);
        public Task<Athlete> CreateAthleteAsync(Athlete athlete);
        public Task<Athlete?> UpdateAthleteAsync(Guid id, Athlete athlete);
        public Task<Athlete?> DeleteAthleteAsync(Guid id);
    }
}
