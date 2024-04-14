using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class ErrorResponse : IResponse
{
    private readonly string message;
    public ErrorResponse(string message)
    {
        this.message = message;
    }
    
    public string GetResponse()
    {
        return this.message;
    }
}