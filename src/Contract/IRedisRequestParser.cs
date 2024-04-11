using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public interface IRedisRequestParser
{
    RedisResponse Parse(string request);
}