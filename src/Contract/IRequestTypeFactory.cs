using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public interface IRequestTypeFactory
{
    RequestType GetRequestType(string request);
}