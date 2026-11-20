using Lab10_JuniorSullca.Application.UsesCases.User.Commands;
using Lab10_JuniorSullca.Application.UsesCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await sender.Send(new GetAllUsersQuery(), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await sender.Send(new GetUserByIdQuery(id), ct);
        if (user == null)
            return NotFound(new { message = "Usuario no encontrado" });

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken ct)
    {
        await sender.Send(command, ct);
        return Ok(new { message = "Usuario creado" });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command, CancellationToken ct)
    {
        command.UserId = id;
        var updated = await sender.Send(command, ct);
        if (updated == null)
            return NotFound(new { message = "Usuario no encontrado para actualizar" });

        return Ok(new { message = "Usuario actualizado" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteUserCommand(id), ct);
        return Ok(new { message = "Usuario eliminado" });
    }
}
