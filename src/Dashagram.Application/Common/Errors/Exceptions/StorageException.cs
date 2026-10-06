namespace Dashagram.Application.Common.Errors.Exceptions
{
    /// <summary>
    /// Exception thrown when there is an error related to storage operations.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public class StorageException(string message, Exception? innerException = null) : Exception(message, innerException);
}
