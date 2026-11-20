using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using Lab10_JuniorSullca.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lab10_JuniorSullca.Infrastructure.Repositories;

public class TicketRepository(AppDbContext context) : GenericRepository<Ticket>(context), ITicketRepository
{
    public async Task<IEnumerable<Ticket>> GetAllWithDetailsAsync()
    {
        return await _context.Tickets
            .AsNoTracking()
            .Include(t => t.User)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
