using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class EchoResponse : IResponse
{
    private readonly string request;
    public EchoResponse(string request)
    {
        this.request = request.TrimEnd();
    }
    
    public string GetResponse()
    {
        var value = this.request.Split("\r\n");
        return value[value.Length - 2];
        return string.Empty;
    }
}