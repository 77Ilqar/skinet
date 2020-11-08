namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message, string details = null): base (statusCode,message)
        {
            Details = details;

        }
        public string Details { get; set; }
    }
}