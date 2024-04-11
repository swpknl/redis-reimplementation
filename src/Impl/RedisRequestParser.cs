using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public class RedisRequestParser : IRedisRequestParser
{
    public RedisResponse Parse(string request)
    {
        var requestType = GetRequestType(request);
        switch (requestType)        
        {
            case RedisRequestType.PING:
                return new RedisResponse("+PONG\r\n");
            case RedisRequestType.ECHO:
                return new RedisResponse("+PONG\r\n");
            default:
                return new RedisResponse("+PONG\r\n");
        }
    }

    private RedisRequestType GetRequestType(string request)
    {
        if (request.Contains("PING"))
        {
            return RedisRequestType.PING;
        }
        else
        {
            return RedisRequestType.ECHO;
        }
        
    }
}