namespace Lab10_JuniorSullca.Application.DTOs;

public record ResponseReportDto
{
    public Guid ResponseId { get; init; }
    public Guid TicketId { get; init; }
    public string TicketTitle { get; init; } = string.Empty;
    public string ResponderUsername { get; init; } = string.Empty;
    public string? ResponderEmail { get; init; }
    public string Message { get; init; } = string.Empty;
    public DateTime? CreatedAt { get; init; }
}
