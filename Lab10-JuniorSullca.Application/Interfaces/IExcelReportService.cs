using Lab10_JuniorSullca.Application.DTOs;

namespace Lab10_JuniorSullca.Application.Interfaces;

public interface IExcelReportService
{
    byte[] GenerateTicketsReport(IEnumerable<TicketReportDto> tickets);
    byte[] GenerateResponsesReport(IEnumerable<ResponseReportDto> responses);
}
