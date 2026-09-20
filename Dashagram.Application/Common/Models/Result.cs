namespace Dashagram.Application.Common.Models
{
    /// <summary>
    /// Represents the result of an operation, including success status, data, errors, and optional metadata.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public record Result<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public List<Error> Errors { get; init; } = [];
        public Meta? Meta { get; init; }

        /// <summary>
        /// Creates a successful result with the provided data and optional metadata.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="meta"></param>
        /// <returns></returns>
        public static Result<T> Ok(T data, Meta? meta = null) =>
            new()
            {
                Success = true,
                Data = data,
                Meta = meta
            };
        
        /// <summary>
        /// Creates a failed result with the provided errors and optional metadata.
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="meta"></param>
        /// <returns></returns>
        public static Result<T> Fail(List<Error> errors, Meta? meta = null) =>
            new()
            { 
                Success = false,
                Errors = errors,
                Meta = meta
            };
    }
}
