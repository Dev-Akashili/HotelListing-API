using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly HotelListingDbContext _context;
    
    public GenericRepository(HotelListingDbContext context)
    {
        _context = context;
    }
    
    public async Task<T> GetAsync(int? id)
    {
        if (id == null)
            return null;
        var result = await _context.Set<T>().FindAsync(id);
           return result;
        }

    public async Task<List<T>> GetAllAsync()
    => await _context.Set<T>().ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await GetAsync(id);
        return entity != null;
    }
}

