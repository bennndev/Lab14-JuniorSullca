using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Role.Queries;

public record GetAllRolesQuery : IRequest<IEnumerable<Domain.Models.Role>>;

internal sealed class GetAllRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesQuery, IEnumerable<Domain.Models.Role>>
{
    public async Task<IEnumerable<Domain.Models.Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Role>().GetAll();
    }
}
