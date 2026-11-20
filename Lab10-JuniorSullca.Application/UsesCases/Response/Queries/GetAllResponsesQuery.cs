using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Queries;

public record GetAllResponsesQuery : IRequest<IEnumerable<Domain.Models.Response>>;

internal sealed class GetAllResponsesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllResponsesQuery, IEnumerable<Domain.Models.Response>>
{
    public async Task<IEnumerable<Domain.Models.Response>> Handle(GetAllResponsesQuery request, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<Domain.Models.Response>().GetAll();
    }
}
