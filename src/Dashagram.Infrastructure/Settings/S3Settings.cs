namespace Dashagram.Infrastructure.Settings
{

    /// <summary>
    /// Represents the settings required for Amazon S3 storage.
    /// </summary>
    public class S3Settings
    {
        public string ServiceUrl { get; set; }
        public string Region { get; set; }
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public string BucketName { get; set; }
        public bool ForcePathStyle { get; set; }
        public string PublicBaseUrl { get; set; }
    }
}
