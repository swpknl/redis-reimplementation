using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public class RedisRequestParser : IRedisRequestParser
{
    public RedisResponse Parse(string request)
    {
        return new RedisResponse("+PONG\r\n");
    }

    private RedisRequestType GetRequestType(string request)
    {
        return RedisRequestType.PING;
    }
}