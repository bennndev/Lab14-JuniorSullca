using System.Security.Cryptography;
using System.Text;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;

public record HashPasswordCommand(string Password) : IRequest<string>;

internal sealed class HashPasswordCommandHandler : IRequestHandler<HashPasswordCommand, string>
{
    public Task<string> Handle(HashPasswordCommand request, CancellationToken cancellationToken)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        return Task.FromResult(Convert.ToHexString(bytes));
    }
}
