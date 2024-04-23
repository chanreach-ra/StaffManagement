using System.Net;

namespace StaffManagementAPI.Exceptions;
public class CustomException : Exception
{
    // Custom properties
    public HttpStatusCode StatusCode { get; }

    // Constructors
    public CustomException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public CustomException(HttpStatusCode statusCode, string message, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
