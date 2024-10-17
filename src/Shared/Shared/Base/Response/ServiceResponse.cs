using System.Text.Json.Serialization;

namespace Shared.Base;

public class ServiceResponse<T>
{
    public T? Data { get; private set; }

    [JsonIgnore]
    public int StatusCode { get; private set; }
    [JsonIgnore]
    public bool IsSuccessful { get; private set; }

    public List<string> Errors { get; set; } = new();

    //static factory methods
    public static ServiceResponse<T> Success(T data, int statusCode)
        => new() { Data = data, StatusCode = statusCode, IsSuccessful = true };

    public static ServiceResponse<NoContent> Success(int statusCode)
        => new() { StatusCode = statusCode, Data = default, IsSuccessful = true };

    public static ServiceResponse<T> Failure(List<string> errors, int statusCode)
        => new() { Errors = errors, StatusCode = statusCode, IsSuccessful = false };

    public static ServiceResponse<T> Failure(string error, int statusCode)
        => new() { Errors = new() { error }, StatusCode = statusCode, IsSuccessful = false };


}