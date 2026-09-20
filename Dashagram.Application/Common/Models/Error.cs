namespace Dashagram.Application.Common.Models
{
    public record Error
    {
        public string Code { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public object? Details { get; init; }
    }
}
