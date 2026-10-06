namespace Dashagram.Application.Common.Models.Storage
{
    public sealed record ImageUpload(Stream Content, string ContentType, long Length);
}
