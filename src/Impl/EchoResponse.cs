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
        var array = this.request.Split("\r\n");
        var value = array[array.Length - 2];
        return $"${value.Length}\r\n{value}\r\n";
    }
}