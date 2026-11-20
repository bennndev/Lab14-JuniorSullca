using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Queries;

public record GetTicketByIdQuery(Guid Id) : IRequest<Domain.Models.Ticket?>;

internal sealed class GetTicketByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTicketByIdQuery, Domain.Models.Ticket?>
{
    public async Task<Domain.Models.Ticket?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Ticket>().GetById(request.Id);
    }
}
