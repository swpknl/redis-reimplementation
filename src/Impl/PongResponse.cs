using codecrafters_redis.Contract;

namespace codecrafters_redis.Impl;

public class PongResponse : IResponse
{
    public string GetResponse()
    {
        return "+PONG\r\n";
    }
}