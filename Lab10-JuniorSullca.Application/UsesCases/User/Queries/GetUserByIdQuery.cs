using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.User.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<Domain.Models.User?>;

internal sealed class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, Domain.Models.User?>
{
    public async Task<Domain.Models.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.User>().GetById(request.Id);
    }
}
