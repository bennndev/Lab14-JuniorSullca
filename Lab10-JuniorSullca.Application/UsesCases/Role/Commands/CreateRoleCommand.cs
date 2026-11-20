using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Role.Commands;

public record CreateRoleCommand : IRequest<Domain.Models.Role>
{
    public string RoleName { get; set; } = null!;
}

internal sealed class CreateRoleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateRoleCommand, Domain.Models.Role>
{
    public async Task<Domain.Models.Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Domain.Models.Role
        {
            RoleId = Guid.NewGuid(),
            RoleName = request.RoleName
        };
        await unitOfWork.Repository<Domain.Models.Role>().Add(role);
        await unitOfWork.Complete();
        return role;
    }
}
