namespace Lab10_JuniorSullca.Application.DTOs;

public record TicketReportDto
{
    public Guid TicketId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string Status { get; init; } = string.Empty;
    public string RequestedBy { get; init; } = string.Empty;
    public string? UserEmail { get; init; }
    public DateTime? CreatedAt { get; init; }
    public DateTime? ClosedAt { get; init; }
}
