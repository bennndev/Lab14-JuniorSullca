using Lab10_JuniorSullca.Application.UsesCases.Ticket.Commands;
using Lab10_JuniorSullca.Application.UsesCases.Ticket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await sender.Send(new GetAllTicketsQuery(), ct));
    }

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel(CancellationToken ct)
    {
        var report = await sender.Send(new ExportTicketsToExcelQuery(), ct);
        return File(report.Content, report.ContentType, report.FileName);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var ticket = await sender.Send(new GetTicketByIdQuery(id), ct);
        if (ticket == null)
            return NotFound(new { message = "Ticket no encontrado" });

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command, CancellationToken ct)
    {
        await sender.Send(command, ct);
        return Ok(new { message = "Ticket creado" });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketCommand command, CancellationToken ct)
    {
        command.TicketId = id;
        var updated = await sender.Send(command, ct);
        if (updated == null)
            return NotFound(new { message = "Ticket no encontrado para actualizar" });

        return Ok(new { message = "Ticket actualizado" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteTicketCommand(id), ct);
        return Ok(new { message = "Ticket eliminado" });
    }
}
