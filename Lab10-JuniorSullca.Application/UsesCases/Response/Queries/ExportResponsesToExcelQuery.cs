using Lab10_JuniorSullca.Application.DTOs;
using Lab10_JuniorSullca.Application.Interfaces;
using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Response.Queries;

public record ExportResponsesToExcelQuery : IRequest<FileReportDto>;

internal sealed class ExportResponsesToExcelQueryHandler(
    IUnitOfWork unitOfWork,
    IExcelReportService excelReportService) : IRequestHandler<ExportResponsesToExcelQuery, FileReportDto>
{
    public async Task<FileReportDto> Handle(ExportResponsesToExcelQuery request, CancellationToken cancellationToken)
    {
        var responses = await unitOfWork.Responses.GetAllWithDetailsAsync();

        var dtoList = responses.Select(r => new ResponseReportDto
        {
            ResponseId = r.ResponseId,
            TicketId = r.TicketId,
            TicketTitle = r.Ticket?.Title ?? "N/A",
            ResponderUsername = r.Responder?.Username ?? "N/A",
            ResponderEmail = r.Responder?.Email,
            Message = r.Message,
            CreatedAt = r.CreatedAt
        }).ToList();

        var fileContent = excelReportService.GenerateResponsesReport(dtoList);
        var fileName = $"Reporte_Respuestas_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx";
        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return new FileReportDto(fileContent, contentType, fileName);
    }
}
