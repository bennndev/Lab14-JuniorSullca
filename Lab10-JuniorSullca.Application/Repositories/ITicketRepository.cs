using Lab10_JuniorSullca.Domain.Models;

namespace Lab10_JuniorSullca.Application.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetAllWithDetailsAsync();
}
