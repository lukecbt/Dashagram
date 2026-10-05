using Dashagram.Application.Common.Models.Storage;

namespace Dashagram.Application.Common.Interfaces.Services
{
    public interface IStorageService
    {
        Task<StorageResult> UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default);
        Task<StorageResult> DeleteAsync(string key, CancellationToken cancellationToken = default);
        Task<StorageResult> DeleteManyAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default);
        string GetPublicUrl(string key);
    }
}
