using Microsoft.EntityFrameworkCore;
using SwimCheck.API.Data;
using SwimCheck.API.Models.Domain;

namespace SwimCheck.API.Repositories
{
    public class SQLAthleteRepository : IAthleteRepository
    {
        private readonly SwimCheckDbContext dbContext;
        public SQLAthleteRepository(SwimCheckDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Athlete> CreateAthleteAsync(Athlete athlete)
        {
            await dbContext.Athletes.AddAsync(athlete);
            await dbContext.SaveChangesAsync();
            return athlete;
        }

        public async Task<List<Athlete>> GetAllAthletesAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            //return await dbContext.Athletes.ToListAsync(); //old code without filtering, sorting, pagination
            var athletes = dbContext.Athletes.AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    athletes = athletes.Where(x => x.Name.Contains(filterQuery));
                }
                else if
                    (filterOn.Equals("Club", StringComparison.OrdinalIgnoreCase))
                {
                    athletes = athletes.Where(x => x.Club != null && x.Club.Contains(filterQuery)); //we verify if Club is not null first because of nullable field property declaration
                }
            }

            //Sorting
            if (string.IsNullOrEmpty(sortBy) == false)
            {
                //Sort by Name
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    athletes = isAscending ? athletes.OrderBy(x => x.Name) : athletes.OrderByDescending(x => x.Name);
                }
                //Sort by Club
                else if (sortBy.Equals("Club", StringComparison.OrdinalIgnoreCase))
                {
                    athletes = isAscending ? athletes.OrderBy(x => x.Club) : athletes.OrderByDescending(x => x.Club);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await athletes.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Athlete?> GetAthleteByIdAsync(Guid id)
        {
            return await dbContext.Athletes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        //public Task<Athlete?> UpdateAthleteAsync(Guid id, Athlete athlete)
        //{
        //    return
        //}

        //public Task<Athlete?> DeleteAthleteAsync(Guid id)
        //{
        //    return
        //}
    }
}
