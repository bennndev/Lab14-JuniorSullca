using System.Collections;
using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Infrastructure.Persistence;

namespace Lab10_JuniorSullca.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repositories;
    private readonly AppDbContext _context;
    private ITicketRepository? _ticketRepository;
    private IResponseRepository? _responseRepository;
    
    public  UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);
    public IResponseRepository Responses => _responseRepository ??= new ResponseRepository(_context);
    
    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
        {
            return (IGenericRepository<TEntity>)_repositories[type];
        }

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }
        throw new Exception($"No se ha podido crear la instancia del repositorio para el tipo {type}");
    }
}
