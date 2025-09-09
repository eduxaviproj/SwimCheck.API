using Microsoft.EntityFrameworkCore;
using SwimCheck.API.Data;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Models.Domain.Enum;
using SwimCheck.API.Repositories.Interfaces;

namespace SwimCheck.API.Repositories.Repos
{
    public class SQLRaceRepository : IRaceRepository
    {
        private readonly SwimCheckDbContext dbContext;
        public SQLRaceRepository(SwimCheckDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Race>> GetAllRacesAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var races = dbContext.Races.AsNoTracking().AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Stroke", StringComparison.OrdinalIgnoreCase))
                {
                    if (Enum.TryParse<Stroke>(filterQuery, true, out var stroke))
                        races = races.Where(r => r.Stroke == stroke);
                }
                else if (filterOn.Equals("Distance", StringComparison.OrdinalIgnoreCase) ||
                         filterOn.Equals("DistanceMeters", StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(filterQuery, out var distance))
                        races = races.Where(r => r.DistanceMeters == distance);
                }
            }

            //Sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                if (sortBy.Equals("Stroke", StringComparison.OrdinalIgnoreCase))
                {
                    //alfabetic order by Enum Stroke values
                    races = isAscending ?
                        races.OrderBy(x => x.Stroke.ToString())
                        : races.OrderByDescending(x => x.Stroke.ToString());
                }
                else if (sortBy.Equals("Distance", StringComparison.OrdinalIgnoreCase) ||
                         sortBy.Equals("DistanceMeters", StringComparison.OrdinalIgnoreCase))
                {
                    races = isAscending ?
                        races.OrderBy(x => x.DistanceMeters)
                        : races.OrderByDescending(x => x.DistanceMeters);
                }
            }
            else
            {
                races = races.OrderBy(r => r.DistanceMeters).ThenBy(r => r.Stroke); //default sorting
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await races.Skip(skipResults).Take(pageSize).ToListAsync();
        }
    }
}
