using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Role.Commands;

public record UpdateRoleCommand : IRequest<Domain.Models.Role?>
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}

internal sealed class UpdateRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRoleCommand, Domain.Models.Role?>
{
    public async Task<Domain.Models.Role?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Domain.Models.Role>();
        var existing = await repo.GetById(request.RoleId);
        if (existing == null) return null;

        existing.RoleName = request.RoleName;
        await repo.Update(existing);
        await unitOfWork.Complete();
        return existing;
    }
}
