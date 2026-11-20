using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.User.Commands;

public record UpdateUserCommand : IRequest<Domain.Models.User?>
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Email { get; set; }
}

internal sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Domain.Models.User?>
{
    public async Task<Domain.Models.User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<Domain.Models.User>();
        var existing = await repo.GetById(request.UserId);
        if (existing == null) return null;

        existing.Username = request.Username;
        existing.Email = request.Email;

        await repo.Update(existing);
        await unitOfWork.Complete();
        return existing;
    }
}
