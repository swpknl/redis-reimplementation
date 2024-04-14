using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class NullResponse : IResponse
{
    public string GetResponse()
    {
        return "$-1\r\n";
    }
}