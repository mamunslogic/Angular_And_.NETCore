using System.Net;

namespace ContactsPortal.ResponseModel
{
    public class CustomResponseModel(HttpStatusCode status, string message, object? errors = null)
    {
        public int Status { get; set; } = Convert.ToInt16(status);
        public string Message { get; set; } = message;
        public object? Errors { get; set; } = errors;
    }
}
