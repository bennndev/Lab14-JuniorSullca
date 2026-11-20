using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Commands;

public record UpdateResponseCommand : IRequest<Domain.Models.Response?>
{
    public Guid ResponseId { get; set; }
    public string Message { get; set; } = null!;
}

internal sealed class UpdateResponseCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateResponseCommand, Domain.Models.Response?>
{
    public async Task<Domain.Models.Response?> Handle(UpdateResponseCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Domain.Models.Response>();
        var existing = await repo.GetById(request.ResponseId);
        if (existing == null) return null;

        existing.Message = request.Message;
        await repo.Update(existing);
        await unitOfWork.Complete();
        return existing;
    }
}
