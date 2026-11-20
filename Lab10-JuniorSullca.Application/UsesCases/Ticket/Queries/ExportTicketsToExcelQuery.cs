using Lab10_JuniorSullca.Application.DTOs;
using Lab10_JuniorSullca.Application.Interfaces;
using Lab10_JuniorSullca.Application.Repositories;
using MediatR;

namespace Lab10_JuniorSullca.Application.UsesCases.Ticket.Queries;

public record ExportTicketsToExcelQuery : IRequest<FileReportDto>;

internal sealed class ExportTicketsToExcelQueryHandler(
    IUnitOfWork unitOfWork,
    IExcelReportService excelReportService) : IRequestHandler<ExportTicketsToExcelQuery, FileReportDto>
{
    public async Task<FileReportDto> Handle(ExportTicketsToExcelQuery request, CancellationToken cancellationToken)
    {
        var tickets = await unitOfWork.Tickets.GetAllWithDetailsAsync();

        var dtoList = tickets.Select(t => new TicketReportDto
        {
            TicketId = t.TicketId,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            RequestedBy = t.User?.Username ?? "N/A",
            UserEmail = t.User?.Email,
            CreatedAt = t.CreatedAt,
            ClosedAt = t.ClosedAt
        }).ToList();

        var fileContent = excelReportService.GenerateTicketsReport(dtoList);
        var fileName = $"Reporte_Tickets_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx";
        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return new FileReportDto(fileContent, contentType, fileName);
    }
}
