using Lab10_JuniorSullca.Domain.Models;

namespace Lab10_JuniorSullca.Application.Repositories;

public interface IResponseRepository : IGenericRepository<Response>
{
    Task<IEnumerable<Response>> GetAllWithDetailsAsync();
}
