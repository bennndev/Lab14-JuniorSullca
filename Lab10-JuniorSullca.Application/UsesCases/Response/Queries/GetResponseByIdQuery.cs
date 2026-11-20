using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Queries;

public record GetResponseByIdQuery(Guid Id) : IRequest<Domain.Models.Response?>;

internal sealed class GetResponseByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetResponseByIdQuery, Domain.Models.Response?>
{
    public async Task<Domain.Models.Response?> Handle(GetResponseByIdQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Response>().GetById(request.Id);
    }
}
