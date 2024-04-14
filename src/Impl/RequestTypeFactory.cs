using codecrafters_redis.Contract;
using codecrafters_redis.Models;

namespace codecrafters_redis.Impl;

public class RequestTypeFactory : IRequestTypeFactory
{
    public RequestType GetRequestType(string request)
    {
        if (request.ToLower().Contains("ping"))
        {
            return RequestType.PING;
        }
        else
        {
            return RequestType.ECHO;
        }
    }
}