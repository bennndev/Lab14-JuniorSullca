using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Commands;

public record CreateTicketCommand : IRequest<Domain.Models.Ticket>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = "Open";
}

internal sealed class CreateTicketCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTicketCommand, Domain.Models.Ticket>
{
    public async Task<Domain.Models.Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Domain.Models.Ticket
        {
            TicketId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            CreatedAt = DateTime.UtcNow
        };
        await unitOfWork.Repository<Domain.Models.Ticket>().Add(ticket);
        await unitOfWork.Complete();
        return ticket;
    }
}
