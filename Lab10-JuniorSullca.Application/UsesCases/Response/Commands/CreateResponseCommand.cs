using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Commands;

public record CreateResponseCommand : IRequest<Domain.Models.Response>
{
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = null!;
}

internal sealed class CreateResponseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateResponseCommand, Domain.Models.Response>
{
    public async Task<Domain.Models.Response> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        var response = new Domain.Models.Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };
        await unitOfWork.Repository<Domain.Models.Response>().Add(response);
        await unitOfWork.Complete();
        return response;
    }
}
