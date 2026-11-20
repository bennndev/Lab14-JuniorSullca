using Lab10_JuniorSullca.Application.UsesCases.Response.Queries;
using Lab10_JuniorSullca.Application.UsesCases.Ticket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_JuniorSullca.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Genera y descarga el reporte en Excel de todos los tickets con información detallada.
    /// </summary>
    [HttpGet("tickets/excel")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportTicketsExcel(CancellationToken ct)
    {
        var report = await sender.Send(new ExportTicketsToExcelQuery(), ct);
        return File(report.Content, report.ContentType, report.FileName);
    }

    /// <summary>
    /// Genera y descarga el reporte en Excel de todas las respuestas con datos del ticket y usuario respondedor.
    /// </summary>
    [HttpGet("responses/excel")]
    [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> ExportResponsesExcel(CancellationToken ct)
    {
        var report = await sender.Send(new ExportResponsesToExcelQuery(), ct);
        return File(report.Content, report.ContentType, report.FileName);
    }
}
