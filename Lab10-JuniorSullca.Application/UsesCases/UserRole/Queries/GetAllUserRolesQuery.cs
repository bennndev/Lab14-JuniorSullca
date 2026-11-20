using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.UserRole.Queries;

public record GetAllUserRolesQuery : IRequest<IEnumerable<Domain.Models.UserRole>>;

internal sealed class GetAllUserRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUserRolesQuery, IEnumerable<Domain.Models.UserRole>>
{
    public async Task<IEnumerable<Domain.Models.UserRole>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.UserRole>().GetAll();
    }
}
