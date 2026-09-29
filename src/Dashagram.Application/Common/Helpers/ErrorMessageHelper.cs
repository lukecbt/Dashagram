namespace Dashagram.Application.Common.Helpers
{
    /// <summary>
    /// Error helper for returning content for the error object.
    /// </summary>
    public static class ErrorHelper
    {
        public static string NotFound(string entity) => $"This {entity} could not be found.";

        public static string NotFound(string entity, object id) => $"No {entity} exists with Id: {id}";
    }
}
