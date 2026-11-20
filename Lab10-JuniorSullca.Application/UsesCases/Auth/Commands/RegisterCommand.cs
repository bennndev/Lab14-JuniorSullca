using Lab10_JuniorSullca.Application.Repositories;
using MediatR;
using UserModel = Lab10_JuniorSullca.Domain.Models.User;

namespace Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;

public record RegisterCommand : IRequest<UserModel>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}

internal sealed class RegisterCommandHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<RegisterCommand, UserModel>
{
    public async Task<UserModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ArgumentException("El nombre de usuario y la contraseña son requeridos.");
        }

        var users = await unitOfWork.Repository<UserModel>().GetAll();
        if (users.Any(u => string.Equals(u.Username, request.Username, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("El usuario ya existe.");
        }

        var passwordHash = await sender.Send(new HashPasswordCommand(request.Password), cancellationToken);

        var user = new UserModel
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = passwordHash,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        await unitOfWork.Repository<UserModel>().Add(user);
        await unitOfWork.Complete();

        return user;
    }
}
