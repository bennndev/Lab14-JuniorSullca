using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lab10_JuniorSullca.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
    
    public async Task<T?> GetById(Guid id) => await _context.Set<T>().FindAsync(id);

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }
    
    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task Delete(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
