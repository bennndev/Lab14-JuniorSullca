using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.UserRole.Queries;

public record GetUserRoleByIdQuery(Guid Id) : IRequest<Domain.Models.UserRole?>;

internal sealed class GetUserRoleByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserRoleByIdQuery, Domain.Models.UserRole?>
{
    public async Task<Domain.Models.UserRole?> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.UserRole>().GetById(request.Id);
    }
}
