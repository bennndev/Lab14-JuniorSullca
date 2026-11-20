using Lab10_JuniorSullca.Application.UsesCases.Auth.Queries;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;

public record AuthenticateCommand(string Username, string Password) : IRequest<string?>;

internal sealed class AuthenticateCommandHandler(ISender sender) : IRequestHandler<AuthenticateCommand, string?>
{
    public async Task<string?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return null;
        }

        var isValid = await sender.Send(new ValidateCredentialsQuery(request.Username, request.Password), cancellationToken);
        if (!isValid)
        {
            return null;
        }

        return await sender.Send(new GenerateTokenCommand(request.Username, "Admin"), cancellationToken);
    }
}
