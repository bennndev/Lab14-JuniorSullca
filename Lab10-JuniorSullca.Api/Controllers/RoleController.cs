using Lab10_JuniorSullca.Application.UsesCases.Role.Commands;
using Lab10_JuniorSullca.Application.UsesCases.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await sender.Send(new GetAllRolesQuery(), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var role = await sender.Send(new GetRoleByIdQuery(id), ct);
        if (role == null)
            return NotFound(new { message = "Rol no encontrado" });

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken ct)
    {
        await sender.Send(command, ct);
        return Ok(new { message = "Rol creado" });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleCommand command, CancellationToken ct)
    {
        command.RoleId = id;
        var updated = await sender.Send(command, ct);
        if (updated == null)
            return NotFound(new { message = "Rol no encontrado para actualizar" });

        return Ok(new { message = "Rol actualizado" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteRoleCommand(id), ct);
        return Ok(new { message = "Rol eliminado" });
    }
}
