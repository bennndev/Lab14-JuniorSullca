namespace Lab10_JuniorSullca.Application.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    ITicketRepository Tickets { get; }
    IResponseRepository Responses { get; }
    Task<int> Complete();
}