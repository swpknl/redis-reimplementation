using codecrafters_redis.Impl;
using codecrafters_redis.Models;

namespace codecrafters_redis.Contract;

public class RequestParser : IRequestParser
{
    private readonly IResponseFactory responseFactory;
    public RequestParser()
    {
        this.responseFactory = new ResponseFactory();
    }
    
    public RequestResponse Parse(string request)
    {
        var response = this.responseFactory.GetResponse(request);
        return new RequestResponse(response.GetResponse());
    }
}