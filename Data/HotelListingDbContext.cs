using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data;

public class HotelListingDbContext : DbContext
{
    public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options) { }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Nigeria",
                ShortName = "NG"
            },
            new Country
            {
                Id = 2,
                Name= "United States",
                ShortName = "US"
            },
            new Country
            {
                Id = 3,
                Name= "United Kingdom",
                ShortName = "UK"
            }
        );

        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Lagos Continental",
                Address = "VI",
                CountryId = 1,
                Rating = 5
            },
            new Hotel
            {
                Id = 2,
                Name = "The Orchid Hotel",
                Address = "Nottingham",
                CountryId = 3,
                Rating = 4
            },
            new Hotel
            {
                Id = 3,
                Name = "Sheraton",
                Address = "California",
                CountryId = 2,
                Rating = 6
            }
        );
    }
}

