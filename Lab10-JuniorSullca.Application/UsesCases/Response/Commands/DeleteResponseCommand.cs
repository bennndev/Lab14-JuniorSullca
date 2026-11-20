using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Commands;

public record DeleteResponseCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteResponseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteResponseCommand, bool>
{
    public async Task<bool> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Repository<Domain.Models.Response>().Delete(request.Id);
        await unitOfWork.Complete();
        return true;
    }
}
