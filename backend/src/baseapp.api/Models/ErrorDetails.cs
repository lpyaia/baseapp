using System.Text.Json;

namespace BaseApp.Api.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; }

        public string Message { get; }

        public ErrorDetails(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}