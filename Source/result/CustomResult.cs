namespace CRUD_C_SHARP.Source.result;

public class CustomResult(string message, string httpDetail, int statusCode) : ResultBase(message, httpDetail, statusCode);