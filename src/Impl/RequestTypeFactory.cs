using codecrafters_redis.Contract;
using codecrafters_redis.Models;

namespace codecrafters_redis.Impl;

public class RequestTypeFactory : IRequestTypeFactory
{
    public RequestType GetRequestType(string request)
    {
        request = request.ToLower();
        if (request.Contains("ping"))
        {
            return RequestType.PING;
        }
        else if (request.Contains("get"))
        {
            return RequestType.GET;
        }
        else if (request.Contains("set"))
        {
            return RequestType.SET;
        }
        else if(request.Contains("echo"))
        {
            return RequestType.ECHO;
        }
        else if (request.Contains("type"))
        {
            return RequestType.TYPE;
        }
        else
        {
            return RequestType.NULL;
        }
    }
}