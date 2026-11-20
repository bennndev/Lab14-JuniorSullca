using Lab10_JuniorSullca.Application.Repositories;
using Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Auth.Queries;

public record ValidateCredentialsQuery(string Username, string Password) : IRequest<bool>;

internal sealed class ValidateCredentialsQueryHandler(IUnitOfWork unitOfWork, ISender sender) : IRequestHandler<ValidateCredentialsQuery, bool>
{
    public async Task<bool> Handle(ValidateCredentialsQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Repository<Domain.Models.User>().GetAll();
        var user = users.FirstOrDefault(u => string.Equals(u.Username, request.Username, StringComparison.OrdinalIgnoreCase));

        if (user == null || string.IsNullOrEmpty(user.PasswordHash))
        {
            return false;
        }

        var hashedPassword = await sender.Send(new HashPasswordCommand(request.Password), cancellationToken);
        return string.Equals(user.PasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase)
               || user.PasswordHash == request.Password;
    }
}
