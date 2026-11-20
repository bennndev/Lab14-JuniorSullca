using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;

public record GenerateTokenCommand(string Username, string Role) : IRequest<string>;

internal sealed class GenerateTokenCommandHandler(IConfiguration configuration) : IRequestHandler<GenerateTokenCommand, string>
{
    public Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, request.Role)
        };

        var secret = configuration["JWT:Secret"] ?? "SuperSecretKeyDefault1234567890123456";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var issuer = configuration["JWT:Issuer"];
        var audience = configuration["JWT:Audience"];

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
