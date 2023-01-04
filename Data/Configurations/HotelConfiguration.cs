using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
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