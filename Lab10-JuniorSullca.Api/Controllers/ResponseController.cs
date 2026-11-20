using Lab10_JuniorSullca.Application.UsesCases.Response.Commands;
using Lab10_JuniorSullca.Application.UsesCases.Response.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        return Ok(await sender.Send(new GetAllResponsesQuery(), ct));
    }

    [HttpGet("report/excel")]
    public async Task<IActionResult> ExportExcel(CancellationToken ct)
    {
        var report = await sender.Send(new ExportResponsesToExcelQuery(), ct);
        return File(report.Content, report.ContentType, report.FileName);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var response = await sender.Send(new GetResponseByIdQuery(id), ct);
        if (response == null)
            return NotFound(new { message = "Response no encontrada" });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseCommand command, CancellationToken ct)
    {
        await sender.Send(command, ct);
        return Ok(new { message = "Response creada" });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResponseCommand command, CancellationToken ct)
    {
        command.ResponseId = id;
        var updated = await sender.Send(command, ct);
        if (updated == null)
            return NotFound(new { message = "Response no encontrada para actualizar" });

        return Ok(new { message = "Response actualizada" });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await sender.Send(new DeleteResponseCommand(id), ct);
        return Ok(new { message = "Response eliminada" });
    }
}
