using Microsoft.EntityFrameworkCore.Query.Internal;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

      

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request yu've made",
                401 => "Authorize, you are not",
                404 => "resources not found",
                500 => "Errors have made",
                _ => null,
            };
        }
    }
}
