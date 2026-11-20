using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.UserRole.Commands;

public record DeleteUserRoleCommand(Guid UserId) : IRequest<bool>;

internal sealed class DeleteUserRoleCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Repository<Domain.Models.UserRole>().Delete(request.UserId);
        await unitOfWork.Complete();
        return true;
    }
}
