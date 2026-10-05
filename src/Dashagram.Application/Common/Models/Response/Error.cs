using System.Net;

namespace Dashagram.Application.Common.Models.Response
{
    public record Error
    {
        public string Message { get; init; } = string.Empty;
        public object? Details { get; init; }
    }
}
