using Microsoft.EntityFrameworkCore;
using SwimCheck.API.Data;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Repositories.Interfaces;

namespace SwimCheck.API.Repositories.Repos
{
    public class SQLEnrollRepository : IEnrollRepository
    {
        private readonly SwimCheckDbContext db;
        public SQLEnrollRepository(SwimCheckDbContext db) => this.db = db;
        public Task<List<Enroll>> GetAllEnrollsAsync(string? filterOn = null, string? filterQuery = null,
                                                        string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 20)
        {
            var enrolls = db.Enrolls
                .AsNoTracking()
                .Include(e => e.Athlete)
                .Include(e => e.Race)
                .AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("AthleteId", StringComparison.OrdinalIgnoreCase))
                {
                    if (Guid.TryParse(filterQuery, out var athleteId))
                    {
                        enrolls = enrolls.Where(e => e.AthleteId == athleteId);
                    }
                }
                else if (filterOn.Equals("RaceId", StringComparison.OrdinalIgnoreCase))
                {
                    if (Guid.TryParse(filterQuery, out var raceId))
                    {
                        enrolls = enrolls.Where(e => e.RaceId == raceId);
                    }
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("AthleteId", StringComparison.OrdinalIgnoreCase))
                {
                    enrolls = isAscending
                        ? enrolls.OrderBy(e => e.AthleteId)
                        : enrolls.OrderByDescending(e => e.AthleteId);
                }
                else if (sortBy.Equals("RaceId", StringComparison.OrdinalIgnoreCase))
                {
                    enrolls = isAscending
                        ? enrolls.OrderBy(e => e.RaceId)
                        : enrolls.OrderByDescending(e => e.RaceId);
                }
            }
            else
            {
                enrolls = enrolls.OrderBy(e => e.AthleteId).ThenBy(e => e.RaceId); // Default sorting
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return enrolls.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        //public Task<Enroll?> GetEnrollByIdAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Enroll?> CreateEnrollAsync(Enroll enroll)
        {
            // Check if the athlete exists
            var athleteExists = await db.Athletes.AnyAsync(a => a.Id == enroll.AthleteId);
            if (!athleteExists)
            { return null; }

            // Check if the race exists
            var raceExists = await db.Races.AnyAsync(r => r.Id == enroll.RaceId);
            if (!raceExists)
            { return null; }

            // Check if the athlete is already enrolled in a race (avoid duplicated enrollments)
            var alreadyEnrolled = await db.Enrolls.AnyAsync(e => e.AthleteId == enroll.AthleteId && e.RaceId == enroll.RaceId);
            if (alreadyEnrolled)
            { return null; }

            db.Enrolls.Add(enroll);
            await db.SaveChangesAsync();

            return await db.Enrolls
                           .Include(e => e.Athlete)
                           .Include(e => e.Race)
                           .FirstOrDefaultAsync(e => e.Id == enroll.Id); // data persists not just in memory
        }

        public async Task<Enroll?> GetEnrollByIdAsync(Guid id) // with includes to fill DTOView properly
        {
            var enroll = db.Enrolls.AsNoTracking()
                                   .Include(e => e.Athlete)
                                   .Include(e => e.Race)
                                   .FirstOrDefaultAsync(e => e.Id == id);
            return await enroll;
        }

        public async Task<Enroll?> UpdateEnrollAsync(Guid id, Enroll enroll)
        {
            var existingEnroll = await db.Enrolls.FirstOrDefaultAsync(e => e.Id == id);

            if (existingEnroll == null)
            { return null; }

            // Check if the athlete or race exists / Check FKs
            var existingAthlete = await db.Athletes.AnyAsync(a => a.Id == enroll.AthleteId);
            var existingRace = await db.Races.AnyAsync(r => r.Id == enroll.RaceId);

            if (!existingAthlete || !existingRace)
            { return null; }

            // Avoid duplicated enrollments
            var alreadyEnrolled = await db.Enrolls.AnyAsync(e => e.AthleteId == enroll.AthleteId && e.RaceId == enroll.RaceId && e.Id != id);
            if (alreadyEnrolled)
            { return null; }

            // Update fields
            existingEnroll.AthleteId = enroll.AthleteId;
            existingEnroll.RaceId = enroll.RaceId;

            await db.SaveChangesAsync();

            return await db.Enrolls
                           .Include(e => e.Athlete)
                           .Include(e => e.Race)
                            .FirstOrDefaultAsync(e => e.Id == existingEnroll.Id); // data persists not just in memory, with includes to be ready to fill DTOView properly
        }

        public async Task<Enroll?> DeleteEnrollAsync(Guid id)
        {
            var enroll = await db.Enrolls.FirstOrDefaultAsync(e => e.Id == id);

            if (enroll == null)
            { return null; }

            db.Enrolls.Remove(enroll);
            await db.SaveChangesAsync();

            return enroll;
        }

    }
}
