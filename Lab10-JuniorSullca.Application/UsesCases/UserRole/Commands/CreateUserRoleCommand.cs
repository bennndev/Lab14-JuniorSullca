using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.UserRole.Commands;

public record CreateUserRoleCommand : IRequest<Domain.Models.UserRole>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

internal sealed class CreateUserRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRoleCommand, Domain.Models.UserRole>
{
    public async Task<Domain.Models.UserRole> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = new Domain.Models.UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = DateTime.UtcNow
        };
        await unitOfWork.Repository<Domain.Models.UserRole>().Add(userRole);
        await unitOfWork.Complete();
        return userRole;
    }
}
