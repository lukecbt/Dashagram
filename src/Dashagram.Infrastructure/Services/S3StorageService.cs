using Amazon.Runtime;
using Amazon.Runtime.Internal.Util;
using Amazon.S3;
using Amazon.S3.Model;
using Dashagram.Application.Common.Errors.Exceptions;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.Common.Models.Storage;
using Dashagram.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Dashagram.Infrastructure.Services
{
    public class S3StorageService(IAmazonS3 s3, IOptions<S3Settings> settings) : IStorageService
    {
        private readonly S3Settings _settings = settings.Value;

        public string GetPublicUrl(string key)
        {
            return $"{_settings.PublicBaseUrl}/{key}";
        }

        public async Task<StorageResult> DeleteAsync(string key, CancellationToken cancellationToken = default)
        {
            try
            {
                await s3.DeleteObjectAsync(_settings.BucketName, key, cancellationToken);
            }
            catch (AmazonServiceException ex)
            {
                //throw new StorageDeleteException($"An error occurred while deleting object from S3. Bucket: {_settings.BucketName} Key: {key}", ex);
                // TODO: Log
            }
            return new StorageResult { Success = true };
        }

        public async Task<StorageResult> DeleteManyAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default)
        {
            DeleteObjectsRequest request = new()
            {
                BucketName = _settings.BucketName,
                Objects = [.. keys.Select(key => new KeyVersion { Key = key })],
                Quiet = true
            };
            try
            {
                DeleteObjectsResponse response = await s3.DeleteObjectsAsync(request, cancellationToken);

                if (response.DeleteErrors.Count > 0)
                {
                    string errors = string.Join(", ", response.DeleteErrors.Select(e => $"{e.Key} ({e.Code})"));
                    //throw new StorageDeleteException($"Failed to delete some objects from S3: {errors}");
                    // TODO: Log
                }
            }
            catch (AmazonServiceException ex)
            {
                //throw new StorageDeleteException($"An error occurred while deleting the objects from S3. Bucket: {_settings.BucketName} Keys: {string.Join(", ", keys.Select(key => key))}", ex);
                // TODO: Log
            }

            return new() { Success = true };
        }

        public async Task<StorageResult> UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default)
        {
            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = key,
                InputStream = content,
                ContentType = contentType,
                DisablePayloadSigning = true
            };

            try
            {
                await s3.PutObjectAsync(request, cancellationToken);
                return new() { Success = true };
            }
            catch (AmazonServiceException ex)
            {
                throw new StorageException($"An error occurred while uploading the object to S3. Bucket: {_settings.BucketName} Key: {key}", ex);
            }
        }
    }
}
