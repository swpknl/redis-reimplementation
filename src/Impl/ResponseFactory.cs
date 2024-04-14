using codecrafters_redis.Contract;
using codecrafters_redis.Models;

namespace codecrafters_redis.Impl;

public class ResponseFactory : IResponseFactory
{
    private readonly IRequestTypeFactory requestTypeFactory;
    public ResponseFactory()
    {
        this.requestTypeFactory = new RequestTypeFactory();
    }
    
    public IResponse GetResponse(string request)
    {
        var requestType = this.requestTypeFactory.GetRequestType(request);
        switch (requestType)        
        {
            case RequestType.ECHO:
                return new EchoResponse(request);
            case RequestType.PING:
                return new PongResponse();
            default:
                return new PongResponse();
        }
    }
}