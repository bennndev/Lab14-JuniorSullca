using Lab10_JuniorSullca.Application.UsesCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var token = await sender.Send(new AuthenticateCommand(request.Username, request.Password), ct);
        if (token == null)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        try
        {
            var user = await sender.Send(new RegisterCommand
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            }, ct);

            return Ok(new
            {
                message = "Usuario registrado exitosamente",
                userId = user.UserId,
                username = user.Username
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}

public class LoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class RegisterRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}
