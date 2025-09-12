using SwimCheck.API.Models.Domain;

namespace SwimCheck.API.Repositories.Interfaces
{
    public interface IEnrollRepository
    {
        public Task<List<Enroll>> GetAllEnrollsAsync(string? filterOn = null, string? filterQuery = null,
                                                        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 20);
        public Task<Enroll?> GetEnrollByIdAsync(Guid id);
        public Task<Enroll?> CreateEnrollAsync(Enroll enroll);
        // public Task<Enroll?> UpdateEnrollAsync(Guid id, Enroll enroll); // just create, read and delete for enrollments
        public Task<Enroll?> DeleteEnrollAsync(Guid id);
    }
}
