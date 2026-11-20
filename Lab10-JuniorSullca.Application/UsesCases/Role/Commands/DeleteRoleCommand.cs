using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Role.Commands;

public record DeleteRoleCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Repository<Domain.Models.Role>().Delete(request.Id);
        await unitOfWork.Complete();
        return true;
    }
}
