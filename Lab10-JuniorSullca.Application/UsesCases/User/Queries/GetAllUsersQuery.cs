using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.User.Queries;

public record GetAllUsersQuery : IRequest<IEnumerable<Domain.Models.User>>;

internal sealed class GetAllUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersQuery, IEnumerable<Domain.Models.User>>
{
    public async Task<IEnumerable<Domain.Models.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.User>().GetAll();
    }
}
