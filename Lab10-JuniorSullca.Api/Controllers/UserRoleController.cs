using Lab10_JuniorSullca.Application.UsesCases.UserRole.Commands;
using Lab10_JuniorSullca.Application.UsesCases.UserRole.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await sender.Send(new GetAllUserRolesQuery(), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var userRole = await sender.Send(new GetUserRoleByIdQuery(id), ct);
        if (userRole == null)
            return NotFound(new { message = "Rol de usuario no encontrado" });

        return Ok(userRole);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleCommand command, CancellationToken ct)
    {
        await sender.Send(command, ct);
        return Ok(new { message = "Rol de usuario asignado" });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRoleCommand command, CancellationToken ct)
    {
        command.UserId = id;
        var updated = await sender.Send(command, ct);
        if (updated == null)
            return NotFound(new { message = "Rol de usuario no encontrado para actualizar" });

        return Ok(new { message = "Rol de usuario actualizado" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteUserRoleCommand(id), ct);
        return Ok(new { message = "Rol de usuario eliminado" });
    }
}
