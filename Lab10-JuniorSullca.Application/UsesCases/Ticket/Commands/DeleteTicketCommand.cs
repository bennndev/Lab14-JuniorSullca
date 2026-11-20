using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Commands;

public record DeleteTicketCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteTicketCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTicketCommand, bool>
{
    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Repository<Domain.Models.Ticket>().Delete(request.Id);
        await unitOfWork.Complete();
        return true;
    }
}
