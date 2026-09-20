namespace Dashagram.Application.Common.Models
{
    public record Meta
    {
        public Guid CorrelationId { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
        public Pagination? Pagination { get; init; }
    }
}
