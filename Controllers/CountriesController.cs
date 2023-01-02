using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountriesRepository _countriesRepository;
    private readonly IMapper _mapper;

    public CountriesController(ICountriesRepository countriesRepository, IMapper mapper)
    {
        _countriesRepository = countriesRepository;
        _mapper = mapper;
    }
    
    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
    {
        var countries = await _countriesRepository.GetAllAsync();
        var result = _mapper.Map<List<GetCountryDto>>(countries);
        return Ok(result);
    }
    
    // GET: api/Countries/id
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDto>> GetCountry(int id)
    {
        var country = await _countriesRepository.GetDetails(id);
        if (country == null)
            return NotFound();
        var result = _mapper.Map<CountryDto>(country);
        return Ok(result);
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<Country>> CreateCountry(CreateCountryDto createCountry)
    {
        var country = _mapper.Map<Country>(createCountry);
        await _countriesRepository.AddAsync(country);
        return NoContent();
    }
    
    // PUT: api/Countries/id
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto country)
    {
        if (id != country.Id)
            return BadRequest("Invalid Record Id");
        
        var result = await _countriesRepository.GetAsync(id);
        
        if (result == null)
            return NotFound();
        
        _mapper.Map(country, result);
        await _countriesRepository.UpdateAsync(result);

        return NoContent();
    }
    
    // DELETE: api/Countries/id
    [HttpDelete]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = _countriesRepository.GetAsync(id);
        if (country == null)
            return NotFound();
        await _countriesRepository.DeleteAsync(id);
        return NoContent();
    }
}

