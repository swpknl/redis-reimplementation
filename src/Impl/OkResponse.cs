using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class OkResponse : IResponse
{
    public string GetResponse()
    {
        return "+OK\r\n";
    }
}