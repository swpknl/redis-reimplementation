using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class KeyResponse : IResponse
{
    private readonly string key;
    public KeyResponse(string key)
    {
        this.key = key;
    }
    public string GetResponse()
    {
         return $"${this.key.Length}\r\n{this.key}\r\n";
    }
}