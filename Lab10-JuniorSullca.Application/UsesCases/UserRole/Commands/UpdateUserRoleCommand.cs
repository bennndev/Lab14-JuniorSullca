using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.UserRole.Commands;

public record UpdateUserRoleCommand : IRequest<Domain.Models.UserRole?>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime? AssignedAt { get; set; }
}

internal sealed class UpdateUserRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserRoleCommand, Domain.Models.UserRole?>
{
    public async Task<Domain.Models.UserRole?> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Domain.Models.UserRole>();
        var existing = await repo.GetById(request.UserId);
        if (existing == null) return null;

        existing.RoleId = request.RoleId;
        existing.AssignedAt = request.AssignedAt ?? DateTime.UtcNow;

        await repo.Update(existing);
        await unitOfWork.Complete();
        return existing;
    }
}
