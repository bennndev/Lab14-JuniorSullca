using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using Lab10_JuniorSullca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lab10_JuniorSullca.Infrastructure.Repositories;

public class ResponseRepository(AppDbContext context) : GenericRepository<Response>(context), IResponseRepository
{
    public async Task<IEnumerable<Response>> GetAllWithDetailsAsync()
    {
        return await _context.Responses
            .AsNoTracking()
            .Include(r => r.Responder)
            .Include(r => r.Ticket)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}
