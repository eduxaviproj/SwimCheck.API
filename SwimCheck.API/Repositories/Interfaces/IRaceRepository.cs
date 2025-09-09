using SwimCheck.API.Models.Domain;

namespace SwimCheck.API.Repositories.Interfaces
{
    public interface IRaceRepository
    {
        public Task<List<Race>> GetAllRacesAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        //public Task<Race?> GetRaceByIdAsync(Guid id);
    }
}
