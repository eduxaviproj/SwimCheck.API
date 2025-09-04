using Microsoft.EntityFrameworkCore;
using SwimCheck.API.Models.Domain;

namespace SwimCheck.API.Data
{
    public class SwimCheckDbContext : DbContext
    {
        public SwimCheckDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Enroll> Enrolls { get; set; }
        public DbSet<Race> Races { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure that an athlete can only enroll once
            modelBuilder.Entity<Enroll>(e => { e.HasIndex(x => new { x.AthleteId, x.RaceId }).IsUnique(); });

            //SEED DATA TO ATHLETE TABLE
            var Athletes = new List<Athlete>
            {
                new Athlete
                {
                    Id = Guid.Parse("b1a12fe5-c033-44e1-abfc-2dc9dcf7579f"),
                    Name = "Eduardo Duarte",
                    BirthDate = new DateTime(1998, 08, 13),
                    Club = "Lousada XXI"
                },
                new Athlete
                {
                    Id = Guid.Parse("e44359e5-41d3-4ac2-a346-235f8e822ff2"),
                    Name = "Michael Phelps",
                    BirthDate = new DateTime(1998, 01, 22),
                    Club = "FC Porto"
                },
                new Athlete
                {
                    Id = Guid.Parse("e6f2bac7-19ef-4085-8857-439eb1ffec5b"),
                    Name = "Caeleb Dressel",
                    BirthDate = new DateTime(1998 / 12 / 31),
                    Club = "SL Benfica"
                }
            };
            modelBuilder.Entity<Athlete>().HasData(Athletes); // Seed the athletes to DB


            //SEED DATA TO RACE TABLE
            var Races = new List<Race>
            {
                new Race
                {
                    Id = Guid.Parse("ed9dca4b-4dbf-4379-96fa-f077e1a9af2a"),
                    Stroke = Stroke.Backstroke,
                    DistanceMeters = 100
                },
                new Race
                {
                    Id = Guid.Parse("0b2e4c57-00e2-4592-b2f5-ecc3098eba8c"),
                    Stroke = Stroke.Medley,
                    DistanceMeters = 200
                },
                new Race
                {
                    Id = Guid.Parse("5088c3f6-1058-449d-9fe5-70815af42061"),
                    Stroke = Stroke.Freestyle,
                    DistanceMeters = 400
                },
                new Race
                {
                    Id = Guid.Parse("073d53ee-5b7c-43ad-9cc8-879e15790bbd"),
                    Stroke = Stroke.Breaststroke,
                    DistanceMeters = 50
                }
            };
            modelBuilder.Entity<Race>(e => { e.Property(x => x.Stroke).HasConversion<string>(); }); // Convert enum to string
            modelBuilder.Entity<Race>().HasData(Races); // Seed the races to DB

        }
    }
}