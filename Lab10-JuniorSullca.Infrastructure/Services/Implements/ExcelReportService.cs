using ClosedXML.Excel;
using Lab10_JuniorSullca.Application.DTOs;
using Lab10_JuniorSullca.Application.Interfaces;

namespace Lab10_JuniorSullca.Infrastructure.Services.Implements;

public class ExcelReportService : IExcelReportService
{
    public byte[] GenerateTicketsReport(IEnumerable<TicketReportDto> tickets)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Tickets");

        // Report Title Banner
        worksheet.Cell(1, 1).Value = "REPORTE GENERAL DE TICKETS";
        var titleRange = worksheet.Range(1, 1, 1, 8);
        titleRange.Merge();
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.FontSize = 14;
        titleRange.Style.Font.FontColor = XLColor.White;
        titleRange.Style.Fill.BackgroundColor = XLColor.FromArgb(24, 43, 73); // Deep Navy
        titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(1).Height = 32;

        // Metadata row
        worksheet.Cell(2, 1).Value = $"Generado el: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        var metaRange = worksheet.Range(2, 1, 2, 8);
        metaRange.Merge();
        metaRange.Style.Font.Italic = true;
        metaRange.Style.Font.FontSize = 9;
        metaRange.Style.Font.FontColor = XLColor.DarkGray;
        metaRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        worksheet.Row(2).Height = 18;

        // Table Headers
        string[] headers =
        [
            "ID Ticket",
            "Título",
            "Descripción",
            "Estado",
            "Solicitante",
            "Email Solicitante",
            "Fecha Creación",
            "Fecha Cierre"
        ];

        const int headerRowIndex = 4;
        for (var i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(headerRowIndex, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontSize = 11;
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = XLColor.FromArgb(41, 128, 185); // Professional Slate Blue
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
        worksheet.Row(headerRowIndex).Height = 26;

        var currentRow = headerRowIndex + 1;
        foreach (var ticket in tickets)
        {
            worksheet.Cell(currentRow, 1).Value = ticket.TicketId.ToString();
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 2).Value = ticket.Title;
            worksheet.Cell(currentRow, 3).Value = ticket.Description ?? "-";

            var statusCell = worksheet.Cell(currentRow, 4);
            statusCell.Value = ticket.Status;
            statusCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            statusCell.Style.Font.Bold = true;

            worksheet.Cell(currentRow, 5).Value = ticket.RequestedBy;
            worksheet.Cell(currentRow, 6).Value = ticket.UserEmail ?? "-";

            if (ticket.CreatedAt.HasValue)
            {
                var createdCell = worksheet.Cell(currentRow, 7);
                createdCell.Value = ticket.CreatedAt.Value;
                createdCell.Style.DateFormat.Format = "yyyy-MM-dd HH:mm:ss";
                createdCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            else
            {
                worksheet.Cell(currentRow, 7).Value = "-";
                worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            if (ticket.ClosedAt.HasValue)
            {
                var closedCell = worksheet.Cell(currentRow, 8);
                closedCell.Value = ticket.ClosedAt.Value;
                closedCell.Style.DateFormat.Format = "yyyy-MM-dd HH:mm:ss";
                closedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            else
            {
                worksheet.Cell(currentRow, 8).Value = "-";
                worksheet.Cell(currentRow, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Zebra striping
            if (currentRow % 2 == 0)
            {
                worksheet.Range(currentRow, 1, currentRow, headers.Length).Style.Fill.BackgroundColor = XLColor.FromArgb(245, 248, 252);
            }

            worksheet.Row(currentRow).Height = 20;
            currentRow++;
        }

        var lastRow = Math.Max(currentRow - 1, headerRowIndex);
        var dataTableRange = worksheet.Range(headerRowIndex, 1, lastRow, headers.Length);
        dataTableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        dataTableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        dataTableRange.Style.Border.OutsideBorderColor = XLColor.LightGray;
        dataTableRange.Style.Border.InsideBorderColor = XLColor.LightGray;

        worksheet.Columns().AdjustToContents();

        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }

    public byte[] GenerateResponsesReport(IEnumerable<ResponseReportDto> responses)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Respuestas");

        // Report Title Banner
        worksheet.Cell(1, 1).Value = "REPORTE GENERAL DE RESPUESTAS A TICKETS";
        var titleRange = worksheet.Range(1, 1, 1, 7);
        titleRange.Merge();
        titleRange.Style.Font.Bold = true;
        titleRange.Style.Font.FontSize = 14;
        titleRange.Style.Font.FontColor = XLColor.White;
        titleRange.Style.Fill.BackgroundColor = XLColor.FromArgb(20, 60, 45); // Deep Forest Green
        titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        worksheet.Row(1).Height = 32;

        // Metadata row
        worksheet.Cell(2, 1).Value = $"Generado el: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        var metaRange = worksheet.Range(2, 1, 2, 7);
        metaRange.Merge();
        metaRange.Style.Font.Italic = true;
        metaRange.Style.Font.FontSize = 9;
        metaRange.Style.Font.FontColor = XLColor.DarkGray;
        metaRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        worksheet.Row(2).Height = 18;

        // Table Headers
        string[] headers =
        [
            "ID Respuesta",
            "ID Ticket",
            "Título del Ticket",
            "Respondido Por",
            "Email Respondedor",
            "Mensaje de Respuesta",
            "Fecha Creación"
        ];

        const int headerRowIndex = 4;
        for (var i = 0; i < headers.Length; i++)
        {
            var cell = worksheet.Cell(headerRowIndex, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontSize = 11;
            cell.Style.Font.FontColor = XLColor.White;
            cell.Style.Fill.BackgroundColor = XLColor.FromArgb(39, 174, 96); // Vibrant Jade Green
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }
        worksheet.Row(headerRowIndex).Height = 26;

        var currentRow = headerRowIndex + 1;
        foreach (var response in responses)
        {
            worksheet.Cell(currentRow, 1).Value = response.ResponseId.ToString();
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 2).Value = response.TicketId.ToString();
            worksheet.Cell(currentRow, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell(currentRow, 3).Value = response.TicketTitle;
            worksheet.Cell(currentRow, 4).Value = response.ResponderUsername;
            worksheet.Cell(currentRow, 5).Value = response.ResponderEmail ?? "-";
            worksheet.Cell(currentRow, 6).Value = response.Message;

            if (response.CreatedAt.HasValue)
            {
                var createdCell = worksheet.Cell(currentRow, 7);
                createdCell.Value = response.CreatedAt.Value;
                createdCell.Style.DateFormat.Format = "yyyy-MM-dd HH:mm:ss";
                createdCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            else
            {
                worksheet.Cell(currentRow, 7).Value = "-";
                worksheet.Cell(currentRow, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            // Zebra striping
            if (currentRow % 2 == 0)
            {
                worksheet.Range(currentRow, 1, currentRow, headers.Length).Style.Fill.BackgroundColor = XLColor.FromArgb(245, 252, 247);
            }

            worksheet.Row(currentRow).Height = 20;
            currentRow++;
        }

        var lastRow = Math.Max(currentRow - 1, headerRowIndex);
        var dataTableRange = worksheet.Range(headerRowIndex, 1, lastRow, headers.Length);
        dataTableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        dataTableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        dataTableRange.Style.Border.OutsideBorderColor = XLColor.LightGray;
        dataTableRange.Style.Border.InsideBorderColor = XLColor.LightGray;

        worksheet.Columns().AdjustToContents();

        using var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }
}
