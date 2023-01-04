using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public AuthManager(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return default;
        var isValidUser = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isValidUser)
            return default;
        return isValidUser;
    }
    
    public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.UserName = userDto.Email;

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "User");

        return result.Errors;
    }
}