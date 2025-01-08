namespace CRUD_C_SHARP.Source.result;

public class ResultBase(string message, string httpDetail, int statusCode) : Exception
{
    public string Message { get; set; } = message;
    public int StatusCode { get; set; } = statusCode;
    public string HttpDetail { get; set; } = httpDetail;
}