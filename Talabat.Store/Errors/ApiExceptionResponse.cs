namespace Talabat.Store.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public string? Details {  get; set; }

        public ApiExceptionResponse(int _statuscode,string?message=null,string?detail=null):base(_statuscode,message)
        {
            Details=detail;
        }
    }
}
