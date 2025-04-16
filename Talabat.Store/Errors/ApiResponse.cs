
namespace Talabat.Store.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiResponse(int _statuscode ,string? _errormessage=null)
        {
            StatusCode = _statuscode ;
            ErrorMessage = _errormessage?? GetDefualtStatusCode(StatusCode) ;
        }

        private string? GetDefualtStatusCode(int statusCode)
        {
            // switch expression
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _=>null

            };
        }
    }
}
