using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Queries;

public record GetAllTicketsQuery : IRequest<IEnumerable<Domain.Models.Ticket>>;

internal sealed class GetAllTicketsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllTicketsQuery, IEnumerable<Domain.Models.Ticket>>
{
    public async Task<IEnumerable<Domain.Models.Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Ticket>().GetAll();
    }
}
