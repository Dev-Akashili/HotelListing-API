using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IHotelsRepository _hotelsRepository;
    private readonly IMapper _mapper;
    
    public HotelsController(IHotelsRepository hotelsRepository, IMapper mapper)
    {
        _hotelsRepository = hotelsRepository;
        _mapper = mapper;
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
    {
        var hotels = await _hotelsRepository.GetAllAsync();
        var result = _mapper.Map<List<HotelDto>>(hotels);
        return Ok(result);
    }
    
    // GET: api/Hotels/id
    [HttpGet("{id}")]
    public async Task<ActionResult<HotelDto>> GetHotel(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);
        
        if (hotel == null)
            return NotFound();
        
        var result = _mapper.Map<HotelDto>(hotel);
        return Ok(result);
    }

    // POST: api/Hotels
    [HttpPost]
    public async Task<ActionResult<Hotel>> CreateHotel(CreateHotelDto hotelDto)
    {
        var hotel = _mapper.Map<Hotel>(hotelDto);
        await _hotelsRepository.AddAsync(hotel);
        return NoContent();
    }

    // PUT: api/Hotels/id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel(int id, HotelDto hotelDto)
    {
        if (id != hotelDto.Id)
            return BadRequest();
        var hotel = await _hotelsRepository.GetAsync(id);
        if (hotel == null)
            return NotFound();
        _mapper.Map(hotelDto, hotel);
        try
        {
            await _hotelsRepository.UpdateAsync(hotel);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _hotelsRepository.Exists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    
    // DELETE: api/Hotels/id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _hotelsRepository.GetAsync(id);
        
        if (hotel == null)
            return NotFound();
        
        await _hotelsRepository.DeleteAsync(id);
        return NoContent();
    }
}