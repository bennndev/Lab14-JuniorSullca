using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Commands;

public record UpdateTicketCommand : IRequest<Domain.Models.Ticket?>
{
    public Guid TicketId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? ClosedAt { get; set; }
}

internal sealed class UpdateTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTicketCommand, Domain.Models.Ticket?>
{
    public async Task<Domain.Models.Ticket?> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Domain.Models.Ticket>();
        var existing = await repo.GetById(request.TicketId);
        if (existing == null) return null;

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.Status = request.Status;
        existing.ClosedAt = request.ClosedAt;

        await repo.Update(existing);
        await unitOfWork.Complete();
        return existing;
    }
}
