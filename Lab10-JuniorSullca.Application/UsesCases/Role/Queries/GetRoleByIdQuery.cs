using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Role.Queries;

public record GetRoleByIdQuery(Guid Id) : IRequest<Domain.Models.Role?>;

internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoleByIdQuery, Domain.Models.Role?>
{
    public async Task<Domain.Models.Role?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Role>().GetById(request.Id);
    }
}
