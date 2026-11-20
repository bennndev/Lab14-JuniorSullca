using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Domain.Models;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.User.Commands;

public record CreateUserCommand : IRequest<Domain.Models.User>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}

internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Domain.Models.User>
{
    public async Task<Domain.Models.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Models.User
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = request.Password,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };
        await unitOfWork.Repository<Domain.Models.User>().Add(user);
        await unitOfWork.Complete();
        return user;
    }
}
