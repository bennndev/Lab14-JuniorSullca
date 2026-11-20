using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.User.Commands;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Repository<Domain.Models.User>().Delete(request.Id);
        await unitOfWork.Complete();
        return true;
    }
}
