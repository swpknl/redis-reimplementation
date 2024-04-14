using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class EchoResponse : IResponse
{
    private readonly string request;
    public EchoResponse(string request)
    {
        this.request = request;
    }
    
    public string GetResponse()
    {
        return string.Empty;
    }
}