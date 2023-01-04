using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(UserDto userDto);
    Task<AuthResponseDto> Login(LoginDto loginDto);
}