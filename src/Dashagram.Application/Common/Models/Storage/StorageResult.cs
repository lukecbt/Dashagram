using Dashagram.Application.Common.Models.Response;

namespace Dashagram.Application.Common.Models.Storage
{
    public class StorageResult
    {
        public bool Success { get; init; }
        public List<Error> Errors { get; init; } = [];
        public Meta? Meta { get; init; }
    }
}
