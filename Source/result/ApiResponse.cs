namespace CRUD_C_SHARP.Source.result;

public class ApiResponse<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? Status { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse(T? data, string? message, string? status, int statusCode)
    {
        Data = data;
        Message = message;
        Status = status;
        StatusCode = statusCode;
    }
}