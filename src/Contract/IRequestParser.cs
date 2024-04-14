using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public interface IRequestParser
{
    RequestResponse Parse(string request);
}