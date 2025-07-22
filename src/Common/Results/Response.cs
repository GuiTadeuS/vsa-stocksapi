namespace StocksApi.Common.Results;

public record Response<T>(int StatusCode, T? Data, int Count);

public class Response
{
    public static Response<T> SuccessResponse<T>(int status, T data, int count = 1)
    {
        return new Response<T>(200, data, count);
    }

    public static Response<T> ErrorResponse<T>(int status, string data = "Internal Server Error")
    {
        return new Response<T>(status, default!, 0);
    }
}
